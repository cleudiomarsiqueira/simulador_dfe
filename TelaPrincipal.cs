using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimuladorDFe.Modelos;
using SimuladorDFe.Servicos;

namespace SimuladorDFe;

public partial class TelaPrincipal : Form
{
    private readonly PreparadorPayloadDfe _preparador = new();
    private readonly HttpClient _httpClient = new();
    private readonly ClienteIntegracaoDfe _cliente;
    private ConfiguracaoDfe _configuracao = ConfiguracaoDfe.Padrao;
    private PayloadPreparado? _payloadPreparado;
    private XmlDocument? _documentoXml;
    private CampoXmlSelecionado? _campoSelecionado;
    private bool _atualizandoDocumento;
    private bool _atualizandoEditorCampo;
    // Mantém o estado em memória para um futuro indicador visual de documento alterado.
    // A referência é definida depois de o arquivo ser normalizado pela aplicação.
    private string? _conteudoOriginalDocumento;
    private bool _documentoAlterado;
    private bool _configuracaoConcluida;
    private bool _operacaoEmAndamento;

    public TelaPrincipal()
    {
        InitializeComponent();
        _cliente = new ClienteIntegracaoDfe(_httpClient);
        AtualizarContextoDocumento();
        AtualizarDisponibilidadeControles();
        Shown += (_, _) => AbrirConfiguracao();
        AtualizarStatus("Configure a URL da API para liberar as demais funções.");
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _httpClient.Dispose();
        base.OnFormClosed(e);
    }

    private TipoDocumento TipoSelecionado => _configuracao.TipoDocumento;
    private FluxoDocumento FluxoSelecionado => _configuracao.FluxoDocumento;

    private void TentarAtualizarTipoDocumento(string conteudo)
    {
        var tipoDocumento = InferirTipoDocumento(conteudo);
        if (tipoDocumento is null || TipoSelecionado == tipoDocumento.Value)
            return;

        AtualizarConfiguracao(_configuracao with
        {
            TipoDocumento = tipoDocumento.Value,
            FluxoDocumento = tipoDocumento == TipoDocumento.MDFe ? FluxoDocumento.Emissao : FluxoSelecionado
        });
    }

    private static TipoDocumento? InferirTipoDocumento(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
            return null;

        if (conteudo.TrimStart().StartsWith('<'))
        {
            try
            {
                var documento = new XmlDocument();
                documento.LoadXml(conteudo);

                return documento.DocumentElement?.LocalName switch
                {
                    "nfeProc" => TipoDocumento.NFe,
                    "cteProc" or "cteSimpProc" => TipoDocumento.CTe,
                    "mdfeProc" => TipoDocumento.MDFe,
                    _ => null
                };
            }
            catch (XmlException)
            {
                return null;
            }
        }

        try
        {
            JToken.Parse(conteudo);
            return TipoDocumento.NFSe;
        }
        catch (JsonReaderException)
        {
            return null;
        }
    }

    private sealed record CampoXmlSelecionado(string Caminho, XmlElement Elemento);

    private void btnAbrirArquivo_Click(object? sender, EventArgs e)
    {
        openFileDialog.Filter = "Documentos fiscais (*.xml;*.json)|*.xml;*.json|Arquivos XML (*.xml)|*.xml|Arquivos JSON (*.json)|*.json|Todos os arquivos (*.*)|*.*";

        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            CarregarArquivo(openFileDialog.FileName);
    }

    private void AbrirConfiguracao()
    {
        using var formulario = new FormularioConfiguracao(_configuracao);
        if (formulario.ShowDialog(this) != DialogResult.OK || formulario.Configuracao is null)
            return;

        AtualizarConfiguracao(formulario.Configuracao);
        _configuracaoConcluida = true;
        AtualizarDisponibilidadeControles();
        AtualizarStatus("Configuração salva. Abra um documento para começar.");
    }

    private void AtualizarConfiguracao(ConfiguracaoDfe configuracao)
    {
        var tipoOuFluxoAlterado = TipoSelecionado != configuracao.TipoDocumento ||
                                  FluxoSelecionado != configuracao.FluxoDocumento;
        _configuracao = configuracao;

        if (tipoOuFluxoAlterado)
        {
            InvalidarPayload();
            _documentoXml = null;
            _campoSelecionado = null;
            arvoreCampos.Nodes.Clear();
            LimparEdicaoCampo();
        }

        AtualizarContextoDocumento();
    }

    private void btnConfigurar_Click(object? sender, EventArgs e) => AbrirConfiguracao();

    private void btnGerarPrevia_Click(object? sender, EventArgs e)
    {
        try
        {
            GerarPrevia();
            tabResultado.SelectedTab = tabJson;
        }
        catch (Exception ex)
        {
            ExibirErro(ex.Message);
        }
    }

    private async void btnEnviar_Click(object? sender, EventArgs e)
    {
        try
        {
            var endereco = MontarEndereco();
            var token = _configuracao.Token;

            if (_payloadPreparado is null)
                GerarPrevia();

            var requisicao = CriarRequisicao();
            txtRequisicao.Text = _cliente.SerializarRequisicao(requisicao);
            tabResultado.SelectedTab = tabResposta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus("Enviando POST para a API...");
            var resposta = await _cliente.EnviarAsync(endereco, token, requisicao, CancellationToken.None);
            txtResposta.Text = MontarResposta(resposta);
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  POST {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");
            AtualizarStatus(resposta.Sucesso
                ? $"POST concluído com HTTP {resposta.CodigoStatus}."
                : $"A API respondeu HTTP {resposta.CodigoStatus}. Consulte a aba Resposta.", !resposta.Sucesso);
        }
        catch (Exception ex)
        {
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  ERRO: {ex.Message}{Environment.NewLine}");
            ExibirErro(ex.Message);
        }
        finally
        {
            AlternarEnvioEmAndamento(false);
        }
    }

    private async void btnColetarPendentes_Click(object? sender, EventArgs e)
    {
        try
        {
            SelecionarFluxoEmissaoParaColeta();
            var endereco = MontarEnderecoColeta();
            var token = _configuracao.Token;
            txtRequisicao.Text = $"GET {endereco.AbsoluteUri}{Environment.NewLine}{Environment.NewLine}(A coleta não possui corpo de requisição.)";
            tabResultado.SelectedTab = tabResposta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus("Coletando documentos pendentes da API...");
            var resposta = await _cliente.ColetarPendentesAsync(endereco, token, CancellationToken.None);
            txtResposta.Text = MontarResposta(resposta);
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  GET pendentes {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");

            if (resposta.Sucesso && TentarPreencherSequenciaColetada(resposta.Conteudo, out var sequenciaDfe))
            {
                _configuracao = _configuracao with { SequenciaDfe = sequenciaDfe };
                AtualizarStatus($"Coleta concluída. A sequência DFe {sequenciaDfe} foi preenchida para confirmação do ACK.");
                return;
            }

            AtualizarStatus(resposta.Sucesso
                ? "Coleta concluída. Informe a sequência DFe retornada antes de confirmar o ACK."
                : $"A API respondeu HTTP {resposta.CodigoStatus}. Consulte a aba Resposta.", !resposta.Sucesso);
        }
        catch (Exception ex)
        {
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  ERRO na coleta: {ex.Message}{Environment.NewLine}");
            ExibirErro(ex.Message);
        }
        finally
        {
            AlternarEnvioEmAndamento(false);
        }
    }

    private async void btnConfirmarAck_Click(object? sender, EventArgs e)
    {
        try
        {
            SelecionarFluxoEmissaoParaColeta();
            var sequenciaDfe = _configuracao.SequenciaDfe ?? 0;
            if (sequenciaDfe <= 0)
                throw new InvalidOperationException("Colete os pendentes primeiro ou informe a sequência DFe para confirmar o ACK.");

            var confirmacao = MessageBox.Show(
                this,
                $"Confirmar o ACK da sequência DFe {sequenciaDfe}? Essa ação altera o status do documento coletado.",
                "Confirmar ACK",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirmacao != DialogResult.Yes)
                return;

            var endereco = MontarEnderecoAcknowledgement(sequenciaDfe);
            var token = _configuracao.Token;
            txtRequisicao.Text = $"POST {endereco.AbsoluteUri}{Environment.NewLine}{Environment.NewLine}(O ACK não possui corpo de requisição.)";
            tabResultado.SelectedTab = tabResposta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus($"Confirmando o ACK da sequência DFe {sequenciaDfe}...");
            var resposta = await _cliente.ConfirmarAcknowledgementAsync(endereco, token, CancellationToken.None);
            txtResposta.Text = MontarResposta(resposta);
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  POST ACK {sequenciaDfe} {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");
            AtualizarStatus(resposta.Sucesso
                ? $"ACK da sequência DFe {sequenciaDfe} confirmado com HTTP {resposta.CodigoStatus}."
                : $"A API respondeu HTTP {resposta.CodigoStatus}. Consulte a aba Resposta.", !resposta.Sucesso);
        }
        catch (Exception ex)
        {
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  ERRO no ACK: {ex.Message}{Environment.NewLine}");
            ExibirErro(ex.Message);
        }
        finally
        {
            AlternarEnvioEmAndamento(false);
        }
    }

    private void btnLimpar_Click(object? sender, EventArgs e)
    {
        DefinirConteudoDocumento(string.Empty);
        txtJson.Clear();
        txtRequisicao.Clear();
        txtResposta.Clear();
        txtHistorico.Clear();
        _configuracao = _configuracao with { SequenciaDfe = null, PontoImpressao = null };
        _documentoXml = null;
        _campoSelecionado = null;
        LimparEstadoDocumento();
        arvoreCampos.Nodes.Clear();
        LimparEdicaoCampo();
        InvalidarPayload();
        AtualizarStatus("Documento e resultados removidos. URL e token foram preservados.");
    }

    private void txtDocumento_DragEnter(object? sender, DragEventArgs e)
    {
        e.Effect = e.Data?.GetDataPresent(DataFormats.FileDrop) == true
            ? DragDropEffects.Copy
            : DragDropEffects.None;
    }

    private void txtDocumento_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetData(DataFormats.FileDrop) is not string[] arquivos || arquivos.Length != 1)
        {
            ExibirErro("Arraste apenas um arquivo por vez.");
            return;
        }

        CarregarArquivo(arquivos[0]);
    }

    private void CarregarArquivo(string caminho)
    {
        try
        {
            var conteudoArquivo = File.ReadAllText(caminho, Encoding.UTF8);
            DefinirConteudoDocumento(conteudoArquivo);
            TentarAtualizarTipoDocumento(conteudoArquivo);
            FormatarDocumentoAtual();
            DefinirDocumentoOriginal();
            SelecionarAbaDocumento();
            AtualizarStatus("Arquivo carregado, formatado e pronto para editar os valores. Gere a prévia antes de enviar.");
        }
        catch (Exception ex)
        {
            ExibirErro($"Não foi possível abrir o arquivo: {ex.Message}");
        }
    }

    private void GerarPrevia()
    {
        if ((TipoSelecionado is TipoDocumento.NFe or TipoDocumento.CTe or TipoDocumento.MDFe) && _documentoXml is null)
            AtualizarEstruturaXml();

        var conteudo = txtDocumento.Text;
        _payloadPreparado = _preparador.Preparar(TipoSelecionado, conteudo);
        txtJson.Text = _payloadPreparado.JsonDocumento;
        txtRequisicao.Text = _cliente.SerializarRequisicao(CriarRequisicao());
        AtualizarStatus("Payload validado localmente: XML/JSON, GZip e Base64 estão prontos para o POST.");
    }

    private RequisicaoRecepcaoDfe CriarRequisicao()
    {
        if (_payloadPreparado is null)
            throw new InvalidOperationException("Gere a prévia do payload antes de montar a requisição.");

        return new RequisicaoRecepcaoDfe
        {
            DocumentoJson = _payloadPreparado.DocumentoJsonCompactado,
            SeqIdentificaDfe = _configuracao.SequenciaDfe,
            PontoImpressao = _configuracao.PontoImpressao
        };
    }

    private Uri MontarEndereco() =>
        RoteamentoDfe.MontarEndereco(_configuracao.UrlBaseApi, RoteamentoDfe.ObterCaminhoRetorno(TipoSelecionado, FluxoSelecionado));

    private Uri MontarEnderecoColeta() =>
        RoteamentoDfe.MontarEndereco(_configuracao.UrlBaseApi, RoteamentoDfe.ObterCaminhoColeta(TipoSelecionado));

    private Uri MontarEnderecoAcknowledgement(int sequenciaDfe) =>
        RoteamentoDfe.MontarEndereco(_configuracao.UrlBaseApi, RoteamentoDfe.ObterCaminhoAcknowledgement(TipoSelecionado, sequenciaDfe));

    private void SelecionarFluxoEmissaoParaColeta()
    {
        if (FluxoSelecionado != FluxoDocumento.Emissao)
            AtualizarConfiguracao(_configuracao with { FluxoDocumento = FluxoDocumento.Emissao });
    }

    private static bool TentarPreencherSequenciaColetada(string conteudo, out int sequenciaDfe)
    {
        sequenciaDfe = 0;

        if (string.IsNullOrWhiteSpace(conteudo))
            return false;

        try
        {
            var sequencias = JToken.Parse(conteudo)
                .SelectTokens("$..seqIdentificaDfe")
                .Where(token => token.Type == JTokenType.Integer)
                .Select(token => token.Value<int>())
                .Distinct()
                .Take(2)
                .ToArray();

            if (sequencias.Length != 1)
                return false;

            sequenciaDfe = sequencias[0];
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }

    private static string MontarResposta(RespostaEnvio resposta)
    {
        var cabecalho = $"HTTP {resposta.CodigoStatus} {resposta.DescricaoStatus}{Environment.NewLine}" +
                        $"Duração: {resposta.Duracao.TotalMilliseconds:N0} ms{Environment.NewLine}{Environment.NewLine}";
        return cabecalho + (string.IsNullOrWhiteSpace(resposta.Conteudo) ? "(A resposta não possui corpo.)" : resposta.Conteudo);
    }

    private void AtualizarContextoDocumento()
    {
        var documentoXml = TipoSelecionado is TipoDocumento.NFe or TipoDocumento.CTe or TipoDocumento.MDFe;
        lblDocumento.Text = documentoXml ? "XML Editável" : "JSON Editável";
        tabConteudoDocumento.Text = documentoXml ? "XML" : "JSON";
        SelecionarAbaDocumento();
    }

    private void SelecionarAbaDocumento() =>
        tabDocumento.SelectedTab = TipoSelecionado == TipoDocumento.NFSe ? tabConteudoDocumento : tabEdicaoCampos;

    private void txtDocumento_TextChanged(object? sender, EventArgs e)
    {
        if (_atualizandoDocumento)
            return;

        AtualizarEstadoAlteracaoDocumento();
        TentarAtualizarTipoDocumento(txtDocumento.Text);
        _documentoXml = null;
        _campoSelecionado = null;
        arvoreCampos.Nodes.Clear();
        LimparEdicaoCampo();
        InvalidarPayload();
    }

    private void txtPesquisarTags_TextChanged(object? sender, EventArgs e)
    {
        if (_documentoXml is not null)
            PreencherArvoreCampos();
    }

    private void FormatarDocumentoAtual()
    {
        if (TipoSelecionado == TipoDocumento.NFSe)
        {
            var json = JToken.Parse(txtDocumento.Text);
            DefinirConteudoDocumento(json.ToString(Newtonsoft.Json.Formatting.Indented));
        }
        else
        {
            AtualizarEstruturaXml();
        }

        // DefinirConteudoDocumento suprime TextChanged durante a formatação.
        // Portanto, invalida explicitamente a prévia para nunca reenviar o arquivo anterior.
        InvalidarPayload();
    }

    private void AtualizarEstruturaXml(string? caminhoSelecionado = null)
    {
        var documento = new XmlDocument { PreserveWhitespace = false };
        documento.LoadXml(txtDocumento.Text);
        _documentoXml = documento;

        DefinirConteudoDocumento(FormatarXml(documento));
        PreencherArvoreCampos(caminhoSelecionado);
    }

    private static string FormatarXml(XmlDocument documento)
    {
        var configuracoes = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            NewLineChars = Environment.NewLine,
            NewLineHandling = NewLineHandling.Replace,
            OmitXmlDeclaration = true
        };

        var texto = new StringBuilder();
        using var escritor = XmlWriter.Create(texto, configuracoes);
        documento.Save(escritor);
        return texto.ToString();
    }

    private void PreencherArvoreCampos(string? caminhoSelecionado = null)
    {
        arvoreCampos.BeginUpdate();
        try
        {
            arvoreCampos.Nodes.Clear();
            _campoSelecionado = null;
            LimparEdicaoCampo();

            if (_documentoXml?.DocumentElement is not XmlElement raiz)
                return;

            var termoPesquisa = txtPesquisarTags.Text.Trim();
            var noRaiz = CriarNoArvore(raiz, $"/{raiz.Name}[1]", termoPesquisa);
            if (noRaiz is null)
                return;

            arvoreCampos.Nodes.Add(noRaiz);
            arvoreCampos.ExpandAll();

            var noParaSelecionar = string.IsNullOrWhiteSpace(caminhoSelecionado)
                ? noRaiz
                : LocalizarNoPorCaminho(arvoreCampos.Nodes, caminhoSelecionado) ?? noRaiz;
            arvoreCampos.SelectedNode = noParaSelecionar;
        }
        finally
        {
            arvoreCampos.EndUpdate();
        }
    }

    private static TreeNode? CriarNoArvore(XmlElement elemento, string caminho, string termoPesquisa)
    {
        var possuiFilhos = elemento.ChildNodes.OfType<XmlElement>().Any();
        var valor = possuiFilhos ? string.Empty : elemento.InnerText.Trim();
        var sufixo = string.IsNullOrEmpty(valor)
            ? string.Empty
            : $" = {valor}";
        var no = new TreeNode(elemento.Name + sufixo)
        {
            Tag = new CampoXmlSelecionado(caminho, elemento)
        };

        var ocorrencias = new Dictionary<string, int>(StringComparer.Ordinal);
        foreach (var filho in elemento.ChildNodes.OfType<XmlElement>())
        {
            ocorrencias.TryGetValue(filho.Name, out var indice);
            indice++;
            ocorrencias[filho.Name] = indice;
            var noFilho = CriarNoArvore(filho, $"{caminho}/{filho.Name}[{indice}]", termoPesquisa);
            if (noFilho is not null)
                no.Nodes.Add(noFilho);
        }

        return string.IsNullOrWhiteSpace(termoPesquisa) ||
               no.Nodes.Count > 0 ||
               CorrespondePesquisa(elemento, caminho, termoPesquisa)
            ? no
            : null;
    }

    private static bool CorrespondePesquisa(XmlElement elemento, string caminho, string termoPesquisa)
    {
        return caminho.Contains(termoPesquisa, StringComparison.OrdinalIgnoreCase) ||
               elemento.Name.Contains(termoPesquisa, StringComparison.OrdinalIgnoreCase) ||
               elemento.InnerText.Contains(termoPesquisa, StringComparison.OrdinalIgnoreCase) ||
               elemento.Attributes.Cast<XmlAttribute>().Any(atributo =>
                   atributo.Name.Contains(termoPesquisa, StringComparison.OrdinalIgnoreCase) ||
                   atributo.Value.Contains(termoPesquisa, StringComparison.OrdinalIgnoreCase));
    }

    private static TreeNode? LocalizarNoPorCaminho(TreeNodeCollection nos, string caminho)
    {
        foreach (TreeNode no in nos)
        {
            if (no.Tag is CampoXmlSelecionado campo && campo.Caminho == caminho)
                return no;

            var encontrado = LocalizarNoPorCaminho(no.Nodes, caminho);
            if (encontrado is not null)
                return encontrado;
        }

        return null;
    }

    private void arvoreCampos_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is not CampoXmlSelecionado campo)
        {
            LimparEdicaoCampo();
            return;
        }

        _atualizandoEditorCampo = true;
        try
        {
            _campoSelecionado = campo;
            var possuiFilhos = campo.Elemento.ChildNodes.OfType<XmlElement>().Any();
            txtCaminhoCampo.Text = campo.Caminho;
            txtValorCampo.Text = possuiFilhos ? string.Empty : campo.Elemento.InnerText;
            txtValorCampo.ReadOnly = possuiFilhos;
            txtValorCampo.PlaceholderText = possuiFilhos
                ? "Grupo: selecione uma tag filha para alterar um valor."
                : string.Empty;

            painelAtributos.SuspendLayout();
            try
            {
                painelAtributos.Controls.Clear();
                var larguraDisponivel = painelAtributos.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 24;
                var larguraCampo = Math.Max(240, larguraDisponivel / 2);
                foreach (XmlAttribute atributo in campo.Elemento.Attributes)
                {
                    var linha = new TableLayoutPanel
                    {
                        AutoSize = false,
                        Margin = new Padding(0, 0, 8, 4),
                        ColumnCount = 1,
                        RowCount = 2,
                        Size = new Size(larguraCampo, 41)
                    };
                    linha.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
                    linha.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                    var rotulo = new Label
                    {
                        AutoEllipsis = true,
                        Dock = DockStyle.Fill,
                        Text = atributo.Name,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    var entrada = new TextBox
                    {
                        Dock = DockStyle.Fill,
                        Tag = atributo,
                        Text = atributo.Value,
                        Margin = new Padding(0, 1, 0, 1)
                    };
                    entrada.TextChanged += entradaAtributo_TextChanged;
                    linha.Controls.Add(rotulo, 0, 0);
                    linha.Controls.Add(entrada, 0, 1);
                    painelAtributos.Controls.Add(linha);
                }
            }
            finally
            {
                painelAtributos.ResumeLayout();
            }
        }
        finally
        {
            _atualizandoEditorCampo = false;
        }
    }

    private void txtValorCampo_TextChanged(object? sender, EventArgs e)
    {
        if (_atualizandoEditorCampo || _campoSelecionado is null || _documentoXml is null || txtValorCampo.ReadOnly)
            return;

        var elemento = _campoSelecionado.Elemento;
        if (!elemento.ChildNodes.OfType<XmlElement>().Any())
            elemento.InnerText = txtValorCampo.Text;

        AplicarAlteracaoAutomatica();
    }

    private void entradaAtributo_TextChanged(object? sender, EventArgs e)
    {
        if (_atualizandoEditorCampo || sender is not TextBox { Tag: XmlAttribute atributo })
            return;

        atributo.Value = ((TextBox)sender).Text;
        AplicarAlteracaoAutomatica();
    }

    private void AplicarAlteracaoAutomatica()
    {
        if (_campoSelecionado is null || _documentoXml is null)
            return;

        var caminho = _campoSelecionado.Caminho;
        DefinirConteudoDocumento(FormatarXml(_documentoXml));
        AtualizarEstadoAlteracaoDocumento();
        AtualizarTextoNoArvore(caminho);
        InvalidarPayload();
        AtualizarStatus($"Alteração aplicada automaticamente: {caminho}. Gere a prévia antes de enviar.");
    }

    private void AtualizarTextoNoArvore(string caminho)
    {
        var no = LocalizarNoPorCaminho(arvoreCampos.Nodes, caminho);
        if (no?.Tag is not CampoXmlSelecionado campo || campo.Elemento.ChildNodes.OfType<XmlElement>().Any())
            return;

        var valor = campo.Elemento.InnerText.Trim();
        no.Text = string.IsNullOrEmpty(valor)
            ? campo.Elemento.Name
            : $"{campo.Elemento.Name} = {valor}";
    }

    private void LimparEdicaoCampo()
    {
        _atualizandoEditorCampo = true;
        try
        {
            txtCaminhoCampo.Clear();
            txtValorCampo.Clear();
            txtValorCampo.ReadOnly = true;
            txtValorCampo.PlaceholderText = "Selecione uma tag na árvore.";
            painelAtributos.Controls.Clear();
        }
        finally
        {
            _atualizandoEditorCampo = false;
        }
    }

    private void DefinirConteudoDocumento(string conteudo)
    {
        _atualizandoDocumento = true;
        try
        {
            txtDocumento.Text = conteudo;
        }
        finally
        {
            _atualizandoDocumento = false;
        }
    }

    private void DefinirDocumentoOriginal()
    {
        _conteudoOriginalDocumento = txtDocumento.Text;
        _documentoAlterado = false;
    }

    private void LimparEstadoDocumento()
    {
        _conteudoOriginalDocumento = null;
        _documentoAlterado = false;
    }

    private void AtualizarEstadoAlteracaoDocumento()
    {
        _documentoAlterado = _conteudoOriginalDocumento is null
            ? !string.IsNullOrWhiteSpace(txtDocumento.Text)
            : !string.Equals(txtDocumento.Text, _conteudoOriginalDocumento, StringComparison.Ordinal);
    }

    private void InvalidarPayload()
    {
        _payloadPreparado = null;
        txtJson.Clear();
        txtRequisicao.Clear();
    }

    private void AlternarEnvioEmAndamento(bool emAndamento)
    {
        _operacaoEmAndamento = emAndamento;
        UseWaitCursor = emAndamento;
        AtualizarDisponibilidadeControles();
    }

    private void AtualizarDisponibilidadeControles()
    {
        var liberado = _configuracaoConcluida && !_operacaoEmAndamento;
        btnConfigurar.Enabled = !_operacaoEmAndamento;
        btnAbrirArquivo.Enabled = liberado;
        btnGerarPrevia.Enabled = liberado;
        btnEnviar.Enabled = liberado;
        btnColetarPendentes.Enabled = liberado;
        btnConfirmarAck.Enabled = liberado;
        btnLimpar.Enabled = liberado;
        splitConteudo.Enabled = liberado;
    }

    private void AtualizarStatus(string mensagem, bool erro = false)
    {
        lblStatus.Text = mensagem;
        lblStatus.ForeColor = erro ? Color.Firebrick : SystemColors.ControlText;
    }

    private void ExibirErro(string mensagem)
    {
        AtualizarStatus(mensagem, erro: true);
        MessageBox.Show(this, mensagem, "Simulador DFe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void TelaPrincipal_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.O)
        {
            btnAbrirArquivo.PerformClick();
            ConsumirAtalho(e);
            return;
        }

        switch (e.KeyCode)
        {
            case Keys.F2:
                btnAbrirArquivo.PerformClick();
                break;
            case Keys.F3:
                btnConfigurar.PerformClick();
                break;
            case Keys.F4:
                btnColetarPendentes.PerformClick();
                break;
            case Keys.F5:
                btnGerarPrevia.PerformClick();
                break;
            case Keys.F8:
                btnConfirmarAck.PerformClick();
                break;
            case Keys.F10:
                btnEnviar.PerformClick();
                break;
            case Keys.F12:
                btnLimpar.PerformClick();
                break;
            default:
                return;
        }

        ConsumirAtalho(e);
    }

    private static void ConsumirAtalho(KeyEventArgs e)
    {
        e.Handled = true;
        e.SuppressKeyPress = true;
    }
}
