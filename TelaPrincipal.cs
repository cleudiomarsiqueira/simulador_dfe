using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimuladorDFe.Modelos;
using SimuladorDFe.Servicos;

namespace SimuladorDFe;

public partial class TelaPrincipal : Form
{
    private static readonly TimeSpan TempoMaximoRequisicao = TimeSpan.FromSeconds(5);

    private enum ResultadoRequisicao
    {
        Neutro,
        Sucesso,
        Erro
    }

    private readonly PreparadorPayloadDfe _preparador = new();
    private readonly HttpClient _httpClient = new();
    private readonly ClienteIntegracaoDfe _cliente;
    private ConfiguracaoDfe _configuracao = ConfiguracaoDfe.Padrao;
    private TipoDocumento _tipoDocumentoColeta = TipoDocumento.NFe;
    private int? _sequenciaDfeColeta;
    private PayloadPreparado? _payloadPreparado;
    private XmlDocument? _documentoXml;
    private CampoXmlSelecionado? _campoSelecionado;
    private bool _atualizandoDocumento;
    private bool _atualizandoEditorCampo;
    private bool _atualizandoParametrosRetorno;
    private bool _atualizandoParametrosColeta;
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
        AtualizarParametrosRetorno();
        AtualizarParametrosColeta();
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
        using var formulario = new FormularioConfiguracao(new ConfiguracaoApi(_configuracao.UrlBaseApi, _configuracao.Token));
        if (formulario.ShowDialog(this) != DialogResult.OK || formulario.Configuracao is null)
            return;

        AtualizarConfiguracao(_configuracao with
        {
            UrlBaseApi = formulario.Configuracao.UrlBaseApi,
            Token = formulario.Configuracao.Token
        });
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

        AtualizarParametrosRetorno();
        AtualizarContextoDocumento();
        AtualizarPreviaAutomaticamente();
        AtualizarDisponibilidadeControles();
    }

    private void menuFerramentasConfigurar_Click(object? sender, EventArgs e) => AbrirConfiguracao();

    private void menuArquivoLimpar_Click(object? sender, EventArgs e)
    {
        if (tabProcessos.SelectedTab == tabColeta)
            LimparColeta();
        else
            LimparRetorno();
    }

    private void menuArquivoSair_Click(object? sender, EventArgs e) => Close();

    private void menuEditarValores_Click(object? sender, EventArgs e)
    {
        tabProcessos.SelectedTab = tabRetorno;
        tabDocumento.SelectedTab = tabEdicaoCampos;
    }

    private void menuEditarDocumento_Click(object? sender, EventArgs e)
    {
        tabProcessos.SelectedTab = tabRetorno;
        tabDocumento.SelectedTab = tabConteudoDocumento;
    }

    private void menuAjudaSobre_Click(object? sender, EventArgs e)
    {
        using var formulario = new FormularioSobre();
        formulario.ShowDialog(this);
    }

    private void tabProcessos_SelectedIndexChanged(object? sender, EventArgs e) => AtualizarDisponibilidadeControles();

    private void tabResultado_SelectedIndexChanged(object? sender, EventArgs e) =>
        SelecionarTextoDaAba(tabResultado.SelectedTab switch
        {
            var aba when aba == tabJson => txtJson,
            var aba when aba == tabRequisicao => txtRequisicao,
            var aba when aba == tabResposta => txtResposta,
            var aba when aba == tabHistorico => txtHistorico,
            _ => null
        });

    private void tabResultadoColeta_SelectedIndexChanged(object? sender, EventArgs e) =>
        SelecionarTextoDaAba(tabResultadoColeta.SelectedTab switch
        {
            var aba when aba == tabRequisicaoColeta => txtRequisicaoColeta,
            var aba when aba == tabRespostaColeta => txtRespostaColeta,
            var aba when aba == tabHistoricoColeta => txtHistoricoColeta,
            _ => null
        });

    private static void SelecionarTextoDaAba(RichTextBox? texto)
    {
        if (texto is null || texto.TextLength == 0)
            return;

        texto.Focus();
        texto.SelectAll();
    }

    private void AtualizarParametrosRetorno()
    {
        _atualizandoParametrosRetorno = true;
        try
        {
            cmbTipoDocumentoRetorno.SelectedIndex = (int)TipoSelecionado;
            cmbFluxoRetorno.SelectedIndex = (int)FluxoSelecionado;
            cmbFluxoRetorno.Enabled = TipoSelecionado != TipoDocumento.MDFe;
            nudSequenciaRetorno.Value = _configuracao.SequenciaDfe ?? 0;
            txtPontoImpressaoRetorno.Text = _configuracao.PontoImpressao ?? string.Empty;
        }
        finally
        {
            _atualizandoParametrosRetorno = false;
        }
    }

    private void AtualizarParametrosColeta()
    {
        _atualizandoParametrosColeta = true;
        try
        {
            cmbTipoDocumentoColeta.SelectedIndex = (int)_tipoDocumentoColeta;
        }
        finally
        {
            _atualizandoParametrosColeta = false;
        }
    }

    private void cmbTipoDocumentoRetorno_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_atualizandoParametrosRetorno || cmbTipoDocumentoRetorno.SelectedIndex < 0)
            return;

        var tipoDocumento = (TipoDocumento)cmbTipoDocumentoRetorno.SelectedIndex;
        AtualizarConfiguracao(_configuracao with
        {
            TipoDocumento = tipoDocumento,
            FluxoDocumento = tipoDocumento == TipoDocumento.MDFe ? FluxoDocumento.Emissao : FluxoSelecionado
        });
    }

    private void cmbFluxoRetorno_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_atualizandoParametrosRetorno || cmbFluxoRetorno.SelectedIndex < 0)
            return;

        AtualizarConfiguracao(_configuracao with { FluxoDocumento = (FluxoDocumento)cmbFluxoRetorno.SelectedIndex });
    }

    private void nudSequenciaRetorno_ValueChanged(object? sender, EventArgs e)
    {
        if (_atualizandoParametrosRetorno)
            return;

        AtualizarConfiguracao(_configuracao with
        {
            SequenciaDfe = nudSequenciaRetorno.Value > 0 ? decimal.ToInt32(nudSequenciaRetorno.Value) : null
        });
    }

    private void txtPontoImpressaoRetorno_TextChanged(object? sender, EventArgs e)
    {
        if (_atualizandoParametrosRetorno)
            return;

        AtualizarConfiguracao(_configuracao with
        {
            PontoImpressao = string.IsNullOrWhiteSpace(txtPontoImpressaoRetorno.Text)
                ? null
                : txtPontoImpressaoRetorno.Text.Trim()
        });
    }

    private void cmbTipoDocumentoColeta_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_atualizandoParametrosColeta || cmbTipoDocumentoColeta.SelectedIndex < 0)
            return;

        _tipoDocumentoColeta = (TipoDocumento)cmbTipoDocumentoColeta.SelectedIndex;
    }

    private async void btnEnviar_Click(object? sender, EventArgs e)
    {
        try
        {
            var endereco = MontarEndereco();
            var token = _configuracao.Token;

            if (_payloadPreparado is null)
                throw new InvalidOperationException("A prévia automática ainda não está válida. Verifique o documento informado.");

            var requisicao = CriarRequisicao();
            txtRequisicao.Text = _cliente.SerializarRequisicao(requisicao);
            tabResultado.SelectedTab = tabResposta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus("Enviando POST para a API...");
            var resposta = await ExecutarComTimeoutAsync(
                cancellationToken => _cliente.EnviarAsync(endereco, token, requisicao, cancellationToken));
            txtResposta.Text = MontarResposta(resposta);
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  POST {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");
            AtualizarStatus(resposta.Sucesso
                ? $"POST concluído com HTTP {resposta.CodigoStatus}."
                : $"A API respondeu HTTP {resposta.CodigoStatus}. Consulte a aba Resposta.",
                resposta.Sucesso ? ResultadoRequisicao.Sucesso : ResultadoRequisicao.Erro);
        }
        catch (Exception ex)
        {
            var mensagem = TraduzirErroDocumento(ex);
            txtHistorico.AppendText($"{DateTime.Now:HH:mm:ss}  ERRO: {mensagem}{Environment.NewLine}");
            ExibirErro(mensagem);
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
            var endereco = MontarEnderecoColeta(_tipoDocumentoColeta);
            var token = _configuracao.Token;
            _sequenciaDfeColeta = null;
            txtRequisicaoColeta.Text = $"GET {endereco.AbsoluteUri}{Environment.NewLine}{Environment.NewLine}(A coleta não possui corpo de requisição.)";
            tabResultadoColeta.SelectedTab = tabRespostaColeta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus("Coletando documentos pendentes de emissão...");
            var resposta = await ExecutarComTimeoutAsync(
                cancellationToken => _cliente.ColetarPendentesAsync(endereco, token, cancellationToken));
            txtRespostaColeta.Text = MontarResposta(resposta);
            txtHistoricoColeta.AppendText($"{DateTime.Now:HH:mm:ss}  GET pendentes {_tipoDocumentoColeta} {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");

            if (resposta.Sucesso && TentarPreencherSequenciaColetada(resposta.Conteudo, out var sequenciaDfe))
            {
                _sequenciaDfeColeta = sequenciaDfe;
                AtualizarParametrosColeta();
                AtualizarStatus(
                    $"Coleta concluída. A sequência DFe {sequenciaDfe} será sugerida ao confirmar o ACK.",
                    ResultadoRequisicao.Sucesso);
                return;
            }

            AtualizarStatus(resposta.Sucesso
                ? "Coleta concluída. Informe a sequência DFe retornada antes de confirmar o ACK."
                : $"A API respondeu HTTP {resposta.CodigoStatus}. Consulte a aba Resposta.",
                resposta.Sucesso ? ResultadoRequisicao.Sucesso : ResultadoRequisicao.Erro);
        }
        catch (Exception ex)
        {
            txtHistoricoColeta.AppendText($"{DateTime.Now:HH:mm:ss}  ERRO na coleta: {ex.Message}{Environment.NewLine}");
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
            using var formulario = new FormularioConfirmarAck(_tipoDocumentoColeta, _sequenciaDfeColeta);
            if (formulario.ShowDialog(this) != DialogResult.OK || formulario.SequenciaDfe is null)
                return;

            var sequenciaDfe = formulario.SequenciaDfe.Value;
            _sequenciaDfeColeta = sequenciaDfe;
            var endereco = MontarEnderecoAcknowledgement(_tipoDocumentoColeta, sequenciaDfe);
            var token = _configuracao.Token;
            txtRequisicaoColeta.Text = $"POST {endereco.AbsoluteUri}{Environment.NewLine}{Environment.NewLine}(O ACK não possui corpo de requisição.)";
            tabResultadoColeta.SelectedTab = tabRespostaColeta;

            AlternarEnvioEmAndamento(true);
            AtualizarStatus($"Confirmando o recebimento da sequência DFe {sequenciaDfe}...");
            var resposta = await ExecutarComTimeoutAsync(
                cancellationToken => _cliente.ConfirmarAcknowledgementAsync(endereco, token, cancellationToken));
            txtRespostaColeta.Text = MontarResposta(resposta);
            txtHistoricoColeta.AppendText($"{DateTime.Now:HH:mm:ss}  POST ACK {_tipoDocumentoColeta} {sequenciaDfe} {resposta.CodigoStatus} {resposta.DescricaoStatus} ({resposta.Duracao.TotalMilliseconds:N0} ms){Environment.NewLine}");
            AtualizarStatus(resposta.Sucesso
                ? $"Recebimento da sequência DFe {sequenciaDfe} confirmado com HTTP {resposta.CodigoStatus}."
                : $"A API respondeu HTTP {resposta.CodigoStatus}. Consulte a aba Resposta.",
                resposta.Sucesso ? ResultadoRequisicao.Sucesso : ResultadoRequisicao.Erro);
        }
        catch (Exception ex)
        {
            txtHistoricoColeta.AppendText($"{DateTime.Now:HH:mm:ss}  ERRO no ACK: {ex.Message}{Environment.NewLine}");
            ExibirErro(ex.Message);
        }
        finally
        {
            AlternarEnvioEmAndamento(false);
        }
    }

    private void btnLimpar_Click(object? sender, EventArgs e)
    {
        LimparRetorno();
    }

    private void btnSalvarXmlAlterado_Click(object? sender, EventArgs e)
    {
        if (!_documentoAlterado || _documentoXml is null)
            return;

        using var dialogoSalvar = new SaveFileDialog
        {
            AddExtension = true,
            DefaultExt = "xml",
            FileName = "documento-alterado.xml",
            Filter = "Arquivos XML (*.xml)|*.xml|Todos os arquivos (*.*)|*.*",
            OverwritePrompt = true,
            Title = "Salvar XML Alterado"
        };

        if (dialogoSalvar.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            File.WriteAllText(dialogoSalvar.FileName, txtDocumento.Text, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            AtualizarStatus($"XML alterado salvo em {Path.GetFileName(dialogoSalvar.FileName)}.", ResultadoRequisicao.Sucesso);
        }
        catch (Exception ex)
        {
            ExibirErro($"Não foi possível salvar o XML alterado: {ex.Message}");
        }
    }

    private void btnLimparColeta_Click(object? sender, EventArgs e) => LimparColeta();

    private void LimparRetorno()
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
        AtualizarParametrosRetorno();
        AtualizarDisponibilidadeControles();
        AtualizarStatus("Dados do retorno removidos. A configuração compartilhada foi preservada.");
    }

    private void LimparColeta()
    {
        _sequenciaDfeColeta = null;
        AtualizarParametrosColeta();
        txtRequisicaoColeta.Clear();
        txtRespostaColeta.Clear();
        txtHistoricoColeta.Clear();
        AtualizarStatus("Dados da coleta removidos. A configuração compartilhada foi preservada.");
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
            GerarPrevia();
            AtualizarDisponibilidadeControles();
            AtualizarStatus("Arquivo carregado, formatado e com prévia atualizada. Edite os valores ou envie o retorno.");
        }
        catch (Exception ex)
        {
            AtualizarDisponibilidadeControles();
            ExibirErro($"Não foi possível abrir o arquivo: {TraduzirErroDocumento(ex)}");
        }
    }

    private void GerarPrevia()
    {
        if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            throw new InvalidOperationException("Carregue ou cole um XML/JSON antes de enviar o retorno.");

        if ((TipoSelecionado is TipoDocumento.NFe or TipoDocumento.CTe or TipoDocumento.MDFe) && _documentoXml is null)
            AtualizarEstruturaXml();

        var conteudo = txtDocumento.Text;
        _payloadPreparado = _preparador.Preparar(TipoSelecionado, conteudo);
        txtJson.Text = FormatarJsonParaExibicao(_payloadPreparado.JsonDocumento);
        txtRequisicao.Text = _cliente.SerializarRequisicao(CriarRequisicao());
        AtualizarDisponibilidadeControles();
        AtualizarStatus("Prévia atualizada automaticamente: XML/JSON, GZip e Base64 estão prontos para o POST.");
    }

    private void AtualizarPreviaAutomaticamente()
    {
        if (string.IsNullOrWhiteSpace(txtDocumento.Text))
        {
            InvalidarPayload();
            return;
        }

        try
        {
            GerarPrevia();
        }
        catch (XmlException)
        {
            InvalidarPayload();
        }
        catch (JsonReaderException)
        {
            InvalidarPayload();
        }
        catch (ArgumentException)
        {
            InvalidarPayload();
        }
        catch (InvalidOperationException)
        {
            InvalidarPayload();
        }
        finally
        {
            AtualizarDisponibilidadeControles();
        }
    }

    private static string FormatarJsonParaExibicao(string json) =>
        JToken.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);

    private RequisicaoRecepcaoDfe CriarRequisicao()
    {
        if (_payloadPreparado is null)
            throw new InvalidOperationException("A prévia automática ainda não está disponível para o documento informado.");

        return new RequisicaoRecepcaoDfe
        {
            DocumentoJson = _payloadPreparado.DocumentoJsonCompactado,
            SeqIdentificaDfe = _configuracao.SequenciaDfe,
            PontoImpressao = _configuracao.PontoImpressao
        };
    }

    private Uri MontarEndereco() =>
        RoteamentoDfe.MontarEndereco(_configuracao.UrlBaseApi, RoteamentoDfe.ObterCaminhoRetorno(TipoSelecionado, FluxoSelecionado));

    private Uri MontarEnderecoColeta(TipoDocumento tipoDocumento) =>
        RoteamentoDfe.MontarEndereco(_configuracao.UrlBaseApi, RoteamentoDfe.ObterCaminhoColeta(tipoDocumento));

    private Uri MontarEnderecoAcknowledgement(TipoDocumento tipoDocumento, int sequenciaDfe) =>
        RoteamentoDfe.MontarEndereco(_configuracao.UrlBaseApi, RoteamentoDfe.ObterCaminhoAcknowledgement(tipoDocumento, sequenciaDfe));

    private static async Task<T> ExecutarComTimeoutAsync<T>(Func<CancellationToken, Task<T>> operacao)
    {
        using var cancelamento = new CancellationTokenSource(TempoMaximoRequisicao);
        try
        {
            return await operacao(cancelamento.Token);
        }
        catch (OperationCanceledException) when (cancelamento.IsCancellationRequested)
        {
            throw new TimeoutException($"A API não respondeu em até {TempoMaximoRequisicao.TotalSeconds:N0} segundos.");
        }
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
        AtualizarPreviaAutomaticamente();
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
        AtualizarPreviaAutomaticamente();
        AtualizarStatus($"Alteração aplicada e prévia atualizada automaticamente: {caminho}.");
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
        var retornoAtivo = liberado && tabProcessos.SelectedTab == tabRetorno;
        var documentoCarregado = !string.IsNullOrWhiteSpace(txtDocumento.Text);
        menuFerramentasConfigurar.Enabled = !_operacaoEmAndamento;
        menuArquivoAbrir.Enabled = retornoAtivo;
        menuArquivoLimpar.Enabled = liberado;
        menuArquivoSair.Enabled = !_operacaoEmAndamento;
        menuEditarValores.Enabled = retornoAtivo;
        menuEditarDocumento.Enabled = retornoAtivo;
        btnAbrirArquivo.Enabled = retornoAtivo;
        btnEnviar.Enabled = retornoAtivo && documentoCarregado && _payloadPreparado is not null;
        btnSalvarXmlAlterado.Enabled = retornoAtivo && _documentoAlterado && _documentoXml is not null;
        btnColetarPendentes.Enabled = liberado && tabProcessos.SelectedTab == tabColeta;
        btnConfirmarAck.Enabled = liberado && tabProcessos.SelectedTab == tabColeta;
        btnLimpar.Enabled = retornoAtivo;
        btnLimparColeta.Enabled = liberado && tabProcessos.SelectedTab == tabColeta;
        tabProcessos.Enabled = liberado;
    }

    private void AtualizarStatus(string mensagem, ResultadoRequisicao resultado = ResultadoRequisicao.Neutro)
    {
        lblStatus.Text = mensagem;
        lblStatus.ForeColor = resultado == ResultadoRequisicao.Erro ? Color.Firebrick : SystemColors.ControlText;
        lblIndicadorStatus.ForeColor = resultado switch
        {
            ResultadoRequisicao.Sucesso => Color.ForestGreen,
            ResultadoRequisicao.Erro => Color.Firebrick,
            _ => Color.DimGray
        };
        lblIndicadorStatus.ToolTipText = resultado switch
        {
            ResultadoRequisicao.Sucesso => "Última requisição concluída com sucesso.",
            ResultadoRequisicao.Erro => "A última requisição terminou com erro.",
            _ => "Nenhuma requisição concluída nesta sessão."
        };
    }

    private void ExibirErro(string mensagem)
    {
        AtualizarStatus(mensagem, ResultadoRequisicao.Erro);
        MessageBox.Show(this, mensagem, "Simulador DFe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private static string TraduzirErroDocumento(Exception excecao) => excecao switch
    {
        XmlException => "O XML informado é inválido ou está incompleto.",
        JsonReaderException => "O JSON informado é inválido ou está incompleto.",
        _ => excecao.Message
    };

    private void TelaPrincipal_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.O)
        {
            if (tabProcessos.SelectedTab == tabRetorno)
            {
                btnAbrirArquivo.PerformClick();
                ConsumirAtalho(e);
            }

            return;
        }

        switch (e.KeyCode)
        {
            case Keys.F2 when tabProcessos.SelectedTab == tabRetorno:
                btnAbrirArquivo.PerformClick();
                break;
            case Keys.F3:
                menuFerramentasConfigurar.PerformClick();
                break;
            case Keys.F8 when tabProcessos.SelectedTab == tabColeta:
                btnConfirmarAck.PerformClick();
                break;
            case Keys.F10:
                if (tabProcessos.SelectedTab == tabRetorno)
                    btnEnviar.PerformClick();
                else if (tabProcessos.SelectedTab == tabColeta)
                    btnColetarPendentes.PerformClick();
                break;
            case Keys.F12:
                if (tabProcessos.SelectedTab == tabColeta)
                    btnLimparColeta.PerformClick();
                else
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

    private void lblOrientacaoColeta_Click(object? sender, EventArgs e)
    {

    }
}
