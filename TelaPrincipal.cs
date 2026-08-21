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
    private PayloadPreparado? _payloadPreparado;
    private XmlDocument? _documentoXml;
    private CampoXmlSelecionado? _campoSelecionado;
    private bool _atualizandoDocumento;

    public TelaPrincipal()
    {
        InitializeComponent();
        _cliente = new ClienteIntegracaoDfe(_httpClient);
        cmbFluxoDocumento.SelectedIndex = (int)FluxoDocumento.Recepcao;
        cmbTipoDocumento.SelectedIndex = 0;
        AtualizarOrientacaoDocumento();
        AtualizarStatus("Informe a URL da publicação, selecione um documento e informe o token se necessário.");
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _httpClient.Dispose();
        base.OnFormClosed(e);
    }

    private TipoDocumento TipoSelecionado => (TipoDocumento)cmbTipoDocumento.SelectedIndex;
    private FluxoDocumento FluxoSelecionado => (FluxoDocumento)cmbFluxoDocumento.SelectedIndex;

    private static string ObterCaminhoEndpoint(TipoDocumento tipoDocumento, FluxoDocumento fluxoDocumento) => (tipoDocumento, fluxoDocumento) switch
    {
        (TipoDocumento.NFe, FluxoDocumento.Emissao) => "/api/v1/nfe/emissao/documentos/retorno",
        (TipoDocumento.NFe, FluxoDocumento.Recepcao) => "/api/v1/nfe/recepcao/documentos/retorno",
        (TipoDocumento.CTe, FluxoDocumento.Emissao) => "/api/v1/cte/emissao/documentos/retorno",
        (TipoDocumento.CTe, FluxoDocumento.Recepcao) => "/api/v1/cte/recepcao/documentos/retorno",
        (TipoDocumento.NFSe, FluxoDocumento.Emissao) => "/api/v1/nfse/emissao/documentos/retorno",
        (TipoDocumento.NFSe, FluxoDocumento.Recepcao) => "/api/v1/nfse/recepcao/documentos/retorno",
        (TipoDocumento.MDFe, FluxoDocumento.Emissao) => "/api/v1/mdfe/emissao/documentos/retorno",
        _ => throw new ArgumentOutOfRangeException(nameof(fluxoDocumento))
    };

    private static string ObterCaminhoBaseEmissao(TipoDocumento tipoDocumento) => tipoDocumento switch
    {
        TipoDocumento.NFe => "/api/v1/nfe/emissao/documentos",
        TipoDocumento.CTe => "/api/v1/cte/emissao/documentos",
        TipoDocumento.NFSe => "/api/v1/nfse/emissao/documentos",
        TipoDocumento.MDFe => "/api/v1/mdfe/emissao/documentos",
        _ => throw new ArgumentOutOfRangeException(nameof(tipoDocumento))
    };

    private static string ObterCaminhoColeta(TipoDocumento tipoDocumento) =>
        $"{ObterCaminhoBaseEmissao(tipoDocumento)}/pendentes";

    private static string ObterCaminhoAcknowledgement(TipoDocumento tipoDocumento, int sequenciaDfe) =>
        $"{ObterCaminhoBaseEmissao(tipoDocumento)}/{sequenciaDfe}/ack";

    private void TentarAtualizarTipoDocumento(string conteudo)
    {
        var tipoDocumento = InferirTipoDocumento(conteudo);
        if (tipoDocumento is null || TipoSelecionado == tipoDocumento.Value)
            return;

        cmbTipoDocumento.SelectedIndex = (int)tipoDocumento.Value;
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

    private void cmbTipoDocumento_SelectedIndexChanged(object? sender, EventArgs e)
    {
        InvalidarPayload();
        _documentoXml = null;
        _campoSelecionado = null;
        arvoreCampos.Nodes.Clear();
        LimparEdicaoCampo();
        AtualizarFluxosDisponiveis();
        AtualizarOrientacaoDocumento();
        AtualizarEndpointCalculado();
    }

    private void cmbFluxoDocumento_SelectedIndexChanged(object? sender, EventArgs e)
    {
        InvalidarPayload();
        AtualizarOrientacaoDocumento();
        AtualizarEndpointCalculado();
    }

    private void txtUrl_TextChanged(object? sender, EventArgs e) => AtualizarEndpointCalculado();

    private void chkMostrarToken_CheckedChanged(object? sender, EventArgs e) =>
        txtToken.UseSystemPasswordChar = !chkMostrarToken.Checked;

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
            var token = txtToken.Text.Trim();

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
            var token = txtToken.Text.Trim();

            txtEndpoint.Text = endereco.AbsoluteUri;
            txtRequisicao.Text = $"GET {endereco.AbsoluteUri}{Environment.NewLine}{Environment.NewLine}(A coleta não possui corpo de requisição.)";
            tabResultado.SelectedTab = tabResposta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus("Coletando documentos pendentes da API...");
            var resposta = await _cliente.ColetarPendentesAsync(endereco, token, CancellationToken.None);
            txtResposta.Text = MontarResposta(resposta);
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  GET pendentes {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");

            if (resposta.Sucesso && TentarPreencherSequenciaColetada(resposta.Conteudo, out var sequenciaDfe))
            {
                nudSequencia.Value = sequenciaDfe;
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
            var sequenciaDfe = decimal.ToInt32(nudSequencia.Value);
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
            var token = txtToken.Text.Trim();
            txtEndpoint.Text = endereco.AbsoluteUri;
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
        txtArquivo.Clear();
        txtDocumento.Clear();
        txtJson.Clear();
        txtRequisicao.Clear();
        txtResposta.Clear();
        txtHistorico.Clear();
        nudSequencia.Value = 0;
        txtPontoImpressao.Clear();
        _documentoXml = null;
        _campoSelecionado = null;
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
            txtArquivo.Text = caminho;
            DefinirConteudoDocumento(conteudoArquivo);
            TentarAtualizarTipoDocumento(conteudoArquivo);
            FormatarDocumentoAtual();
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
            SeqIdentificaDfe = nudSequencia.Value > 0 ? decimal.ToInt32(nudSequencia.Value) : null,
            PontoImpressao = string.IsNullOrWhiteSpace(txtPontoImpressao.Text) ? null : txtPontoImpressao.Text.Trim()
        };
    }

    private Uri MontarEndereco() => MontarEndereco(ObterCaminhoEndpoint(TipoSelecionado, FluxoSelecionado));

    private Uri MontarEnderecoColeta() => MontarEndereco(ObterCaminhoColeta(TipoSelecionado));

    private Uri MontarEnderecoAcknowledgement(int sequenciaDfe) =>
        MontarEndereco(ObterCaminhoAcknowledgement(TipoSelecionado, sequenciaDfe));

    private Uri MontarEndereco(string caminhoEndpoint)
    {
        var urlPublicacao = txtUrl.Text.Trim().TrimEnd('/');
        if (!Uri.TryCreate(urlPublicacao, UriKind.Absolute, out var publicacao) ||
            publicacao.Scheme is not ("http" or "https") ||
            !string.IsNullOrEmpty(publicacao.Query) || !string.IsNullOrEmpty(publicacao.Fragment))
            throw new InvalidOperationException("Informe somente a URL base HTTP ou HTTPS da publicação, sem rota, parâmetros ou fragmentos.");

        var endereco = new Uri(urlPublicacao + caminhoEndpoint, UriKind.Absolute);

        return endereco;
    }

    private void SelecionarFluxoEmissaoParaColeta()
    {
        if (FluxoSelecionado != FluxoDocumento.Emissao)
            cmbFluxoDocumento.SelectedIndex = (int)FluxoDocumento.Emissao;
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

    private void AtualizarEndpointCalculado()
    {
        try
        {
            txtEndpoint.Text = MontarEndereco().AbsoluteUri;
        }
        catch
        {
            txtEndpoint.Clear();
        }
    }

    private static string MontarResposta(RespostaEnvio resposta)
    {
        var cabecalho = $"HTTP {resposta.CodigoStatus} {resposta.DescricaoStatus}{Environment.NewLine}" +
                        $"Duração: {resposta.Duracao.TotalMilliseconds:N0} ms{Environment.NewLine}{Environment.NewLine}";
        return cabecalho + (string.IsNullOrWhiteSpace(resposta.Conteudo) ? "(A resposta não possui corpo.)" : resposta.Conteudo);
    }

    private void AtualizarOrientacaoDocumento()
    {
        var documentoXml = TipoSelecionado is TipoDocumento.NFe or TipoDocumento.CTe or TipoDocumento.MDFe;
        lblDocumento.Text = documentoXml ? "XML Editável" : "JSON Editável";
        tabConteudoDocumento.Text = documentoXml ? "XML" : "JSON";
        SelecionarAbaDocumento();

        lblOrientacaoDocumento.Text = TipoSelecionado switch
        {
            TipoDocumento.NFe => $"NF-e {ObterDescricaoFluxo()}: selecione um XML nfeProc. Escolha uma tag na árvore para editar valor ou atributos.",
            TipoDocumento.CTe => FluxoSelecionado == FluxoDocumento.Emissao
                ? "CT-e emissão: o retorno será gravado em DFE_RETORNO. Selecione um XML cteProc ou cteSimpProc."
                : "CT-e recepção: selecione um XML cteProc ou cteSimpProc. Escolha uma tag na árvore para editar valor ou atributos.",
            TipoDocumento.NFSe => "NFS-e: selecione o JSON recebido pela integração. Nesse fluxo, a edição é feita diretamente no JSON.",
            TipoDocumento.MDFe => "MDF-e: selecione um XML mdfeProc de retorno. Escolha uma tag na árvore para editar valor ou atributos.",
            _ => string.Empty
        };
    }

    private void SelecionarAbaDocumento() =>
        tabDocumento.SelectedTab = TipoSelecionado == TipoDocumento.NFSe ? tabConteudoDocumento : tabEdicaoCampos;

    private void AtualizarFluxosDisponiveis()
    {
        var aceitaRecepcao = TipoSelecionado != TipoDocumento.MDFe;
        cmbFluxoDocumento.Enabled = aceitaRecepcao;

        if (!aceitaRecepcao && FluxoSelecionado != FluxoDocumento.Emissao)
            cmbFluxoDocumento.SelectedIndex = (int)FluxoDocumento.Emissao;
    }

    private string ObterDescricaoFluxo() =>
        FluxoSelecionado == FluxoDocumento.Emissao ? "emissão" : "recepção";

    private void txtDocumento_TextChanged(object? sender, EventArgs e)
    {
        if (_atualizandoDocumento)
            return;

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

        _campoSelecionado = campo;
        var possuiFilhos = campo.Elemento.ChildNodes.OfType<XmlElement>().Any();
        txtCaminhoCampo.Text = campo.Caminho;
        txtValorCampo.Text = possuiFilhos ? string.Empty : campo.Elemento.InnerText;
        txtValorCampo.ReadOnly = possuiFilhos;
        txtValorCampo.PlaceholderText = possuiFilhos
            ? "Grupo: selecione uma tag filha para alterar um valor."
            : string.Empty;
        btnAplicarCampo.Enabled = true;

        painelAtributos.SuspendLayout();
        try
        {
            painelAtributos.Controls.Clear();
            foreach (XmlAttribute atributo in campo.Elemento.Attributes)
            {
                var linha = new TableLayoutPanel
                {
                    AutoSize = false,
                    ColumnCount = 2,
                    Margin = new Padding(0, 1, 0, 1),
                    Size = new Size(Math.Max(300, painelAtributos.ClientSize.Width - 8), 25)
                };
                linha.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
                linha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                var rotulo = new Label
                {
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
                linha.Controls.Add(rotulo, 0, 0);
                linha.Controls.Add(entrada, 1, 0);
                painelAtributos.Controls.Add(linha);
            }
        }
        finally
        {
            painelAtributos.ResumeLayout();
        }
    }

    private void btnAplicarCampo_Click(object? sender, EventArgs e)
    {
        if (_campoSelecionado is null || _documentoXml is null)
        {
            ExibirErro("Selecione um campo do XML antes de aplicar uma alteração.");
            return;
        }

        var elemento = _campoSelecionado.Elemento;
        if (!elemento.ChildNodes.OfType<XmlElement>().Any())
            elemento.InnerText = txtValorCampo.Text;

        foreach (var entrada in painelAtributos.Controls
                     .OfType<TableLayoutPanel>()
                     .SelectMany(linha => linha.Controls.OfType<TextBox>()))
        {
            if (entrada.Tag is XmlAttribute atributo)
                atributo.Value = entrada.Text;
        }

        var caminho = _campoSelecionado.Caminho;
        DefinirConteudoDocumento(FormatarXml(_documentoXml));
        PreencherArvoreCampos(caminho);
        InvalidarPayload();
        AtualizarStatus($"Campo alterado: {caminho}. Gere a prévia antes de enviar.");
    }

    private void LimparEdicaoCampo()
    {
        txtCaminhoCampo.Clear();
        txtValorCampo.Clear();
        txtValorCampo.ReadOnly = true;
        txtValorCampo.PlaceholderText = "Selecione uma tag na árvore.";
        painelAtributos.Controls.Clear();
        btnAplicarCampo.Enabled = false;
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

    private void InvalidarPayload()
    {
        _payloadPreparado = null;
        txtJson.Clear();
        txtRequisicao.Clear();
    }

    private void AlternarEnvioEmAndamento(bool emAndamento)
    {
        UseWaitCursor = emAndamento;
        btnEnviar.Enabled = !emAndamento;
        btnGerarPrevia.Enabled = !emAndamento;
        btnColetarPendentes.Enabled = !emAndamento;
        btnConfirmarAck.Enabled = !emAndamento;
        btnAbrirArquivo.Enabled = !emAndamento;
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
