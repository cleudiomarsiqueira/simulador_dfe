using SimuladorDFe.Modelos;

namespace SimuladorDFe;

public partial class FormularioConfiguracao : Form
{
    public ConfiguracaoDfe? Configuracao { get; private set; }

    public FormularioConfiguracao(ConfiguracaoDfe configuracao)
    {
        InitializeComponent();

        _txtUrl.Text = configuracao.UrlBaseApi;
        _cmbTipoDocumento.SelectedIndex = (int)configuracao.TipoDocumento;
        _cmbFluxoDocumento.SelectedIndex = (int)configuracao.FluxoDocumento;
        _nudSequencia.Value = configuracao.SequenciaDfe ?? 0;
        _txtPontoImpressao.Text = configuracao.PontoImpressao ?? string.Empty;
        _txtToken.Text = configuracao.Token;
        AtualizarPrevia();
    }

    private void txtUrl_TextChanged(object? sender, EventArgs e) => AtualizarPrevia();

    private void cmbTipoDocumento_SelectedIndexChanged(object? sender, EventArgs e) => AtualizarPrevia();

    private void cmbFluxoDocumento_SelectedIndexChanged(object? sender, EventArgs e) => AtualizarPrevia();

    private void chkMostrarToken_CheckedChanged(object? sender, EventArgs e) =>
        _txtToken.UseSystemPasswordChar = !_chkMostrarToken.Checked;

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        try
        {
            var tipoDocumento = (TipoDocumento)_cmbTipoDocumento.SelectedIndex;
            var fluxoDocumento = (FluxoDocumento)_cmbFluxoDocumento.SelectedIndex;
            RoteamentoDfe.MontarEndereco(_txtUrl.Text, RoteamentoDfe.ObterCaminhoRetorno(tipoDocumento, fluxoDocumento));

            Configuracao = new ConfiguracaoDfe(
                _txtUrl.Text.Trim().TrimEnd('/'),
                tipoDocumento,
                fluxoDocumento,
                _nudSequencia.Value > 0 ? decimal.ToInt32(_nudSequencia.Value) : null,
                string.IsNullOrWhiteSpace(_txtPontoImpressao.Text) ? null : _txtPontoImpressao.Text.Trim(),
                _txtToken.Text.Trim());
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Simulador DFe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtUrl.Focus();
        }
    }

    private void AtualizarPrevia()
    {
        if (_cmbTipoDocumento.SelectedIndex < 0 || _cmbFluxoDocumento.SelectedIndex < 0)
            return;

        var tipoDocumento = (TipoDocumento)_cmbTipoDocumento.SelectedIndex;
        if (tipoDocumento == TipoDocumento.MDFe)
        {
            _cmbFluxoDocumento.SelectedIndex = (int)FluxoDocumento.Emissao;
            _cmbFluxoDocumento.Enabled = false;
        }
        else
        {
            _cmbFluxoDocumento.Enabled = true;
        }

        var fluxoDocumento = (FluxoDocumento)_cmbFluxoDocumento.SelectedIndex;
        try
        {
            _txtEndpoint.Text = RoteamentoDfe.MontarEndereco(
                _txtUrl.Text,
                RoteamentoDfe.ObterCaminhoRetorno(tipoDocumento, fluxoDocumento)).AbsoluteUri;
        }
        catch
        {
            _txtEndpoint.Clear();
        }

        _lblOrientacao.Text = tipoDocumento switch
        {
            TipoDocumento.NFe => $"NF-e {ObterDescricaoFluxo(fluxoDocumento)}: selecione um XML nfeProc.",
            TipoDocumento.CTe => fluxoDocumento == FluxoDocumento.Emissao
                ? "CT-e emissão: o retorno será gravado em DFE_RETORNO."
                : "CT-e recepção: selecione um XML cteProc ou cteSimpProc.",
            TipoDocumento.NFSe => "NFS-e: selecione o JSON recebido pela integração.",
            TipoDocumento.MDFe => "MDF-e: selecione um XML mdfeProc de retorno.",
            _ => string.Empty
        };
    }

    private static string ObterDescricaoFluxo(FluxoDocumento fluxoDocumento) =>
        fluxoDocumento == FluxoDocumento.Emissao ? "emissão" : "recepção";
}
