using SimuladorDFe.Modelos;

namespace SimuladorDFe;

public partial class FormularioConfiguracao : Form
{
    public ConfiguracaoApi? Configuracao { get; private set; }

    public FormularioConfiguracao(ConfiguracaoApi configuracao)
    {
        InitializeComponent();
        _txtUrl.Text = configuracao.UrlBaseApi;
        _txtToken.Text = configuracao.Token;
    }

    private void chkMostrarToken_CheckedChanged(object? sender, EventArgs e) =>
        _txtToken.UseSystemPasswordChar = !_chkMostrarToken.Checked;

    private void btnSalvar_Click(object? sender, EventArgs e)
    {
        try
        {
            RoteamentoDfe.MontarEndereco(_txtUrl.Text, "/");
            Configuracao = new ConfiguracaoApi(_txtUrl.Text.Trim().TrimEnd('/'), _txtToken.Text.Trim());
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Simulador DFe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtUrl.Focus();
        }
    }
}
