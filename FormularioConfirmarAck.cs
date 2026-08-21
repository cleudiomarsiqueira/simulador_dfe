using SimuladorDFe.Modelos;

namespace SimuladorDFe;

public partial class FormularioConfirmarAck : Form
{
    public int? SequenciaDfe { get; private set; }

    public FormularioConfirmarAck(TipoDocumento tipoDocumento, int? sequenciaSugerida)
    {
        InitializeComponent();
        lblOrientacao.Text = $"Informe a sequência DFe de {ObterDescricaoTipo(tipoDocumento)} que receberá o ACK. Somente esta sequência terá o status confirmado.";

        if (sequenciaSugerida is > 0)
            nudSequenciaDfe.Value = sequenciaSugerida.Value;
    }

    private void btnConfirmar_Click(object? sender, EventArgs e)
    {
        if (nudSequenciaDfe.Value <= 0)
        {
            MessageBox.Show(this, "Informe uma sequência DFe maior que zero.", "Confirmar Recebimento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudSequenciaDfe.Focus();
            return;
        }

        SequenciaDfe = decimal.ToInt32(nudSequenciaDfe.Value);
        DialogResult = DialogResult.OK;
    }

    private static string ObterDescricaoTipo(TipoDocumento tipoDocumento) => tipoDocumento switch
    {
        TipoDocumento.NFe => "NF-e",
        TipoDocumento.CTe => "CT-e",
        TipoDocumento.NFSe => "NFS-e",
        TipoDocumento.MDFe => "MDF-e",
        _ => throw new ArgumentOutOfRangeException(nameof(tipoDocumento))
    };
}
