#nullable enable

namespace SimuladorDFe;

partial class FormularioSobre
{
    private System.ComponentModel.IContainer? components = null;
    private TableLayoutPanel tabelaSobre = null!;
    private Label lblTitulo = null!;
    private Label lblDescricao = null!;
    private Label lblCreditos = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabelaSobre = new TableLayoutPanel();
        lblTitulo = new Label();
        lblDescricao = new Label();
        lblCreditos = new Label();
        tabelaSobre.SuspendLayout();
        SuspendLayout();
        //
        // tabelaSobre
        //
        tabelaSobre.ColumnCount = 1;
        tabelaSobre.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tabelaSobre.Controls.Add(lblTitulo, 0, 0);
        tabelaSobre.Controls.Add(lblDescricao, 0, 1);
        tabelaSobre.Controls.Add(lblCreditos, 0, 2);
        tabelaSobre.Dock = DockStyle.Fill;
        tabelaSobre.Padding = new Padding(18, 16, 18, 16);
        tabelaSobre.RowCount = 3;
        tabelaSobre.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tabelaSobre.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaSobre.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        tabelaSobre.TabIndex = 0;
        //
        // lblTitulo
        //
        lblTitulo.AutoSize = true;
        lblTitulo.Dock = DockStyle.Fill;
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Text = "Simulador DFe";
        lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblDescricao
        //
        lblDescricao.Dock = DockStyle.Fill;
        lblDescricao.Text = "Simula retorno, coleta e ACK da Integração DFe em ambientes de teste.";
        lblDescricao.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblCreditos
        //
        lblCreditos.AutoSize = true;
        lblCreditos.Dock = DockStyle.Fill;
        lblCreditos.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblCreditos.Text = "Desenvolvido por Cleudiomar Siqueira";
        lblCreditos.TextAlign = ContentAlignment.MiddleLeft;
        //
        // FormularioSobre
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(510, 180);
        Controls.Add(tabelaSobre);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(526, 219);
        Name = "FormularioSobre";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Sobre o Simulador DFe";
        tabelaSobre.ResumeLayout(false);
        tabelaSobre.PerformLayout();
        ResumeLayout(false);
    }
}
