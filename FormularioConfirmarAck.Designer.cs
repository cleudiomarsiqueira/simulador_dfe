#nullable enable

namespace SimuladorDFe;

partial class FormularioConfirmarAck
{
    private System.ComponentModel.IContainer? components = null;
    private TableLayoutPanel tabelaConfirmacao = null!;
    private Label lblOrientacao = null!;
    private Label lblSequenciaDfe = null!;
    private NumericUpDown nudSequenciaDfe = null!;
    private TableLayoutPanel painelAcoes = null!;
    private Button btnConfirmar = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabelaConfirmacao = new TableLayoutPanel();
        lblOrientacao = new Label();
        lblSequenciaDfe = new Label();
        nudSequenciaDfe = new NumericUpDown();
        painelAcoes = new TableLayoutPanel();
        btnConfirmar = new Button();
        tabelaConfirmacao.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSequenciaDfe).BeginInit();
        painelAcoes.SuspendLayout();
        SuspendLayout();
        //
        // tabelaConfirmacao
        //
        tabelaConfirmacao.ColumnCount = 1;
        tabelaConfirmacao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tabelaConfirmacao.Controls.Add(lblOrientacao, 0, 0);
        tabelaConfirmacao.Controls.Add(lblSequenciaDfe, 0, 1);
        tabelaConfirmacao.Controls.Add(nudSequenciaDfe, 0, 2);
        tabelaConfirmacao.Controls.Add(painelAcoes, 0, 4);
        tabelaConfirmacao.Dock = DockStyle.Fill;
        tabelaConfirmacao.Location = new Point(0, 0);
        tabelaConfirmacao.Name = "tabelaConfirmacao";
        tabelaConfirmacao.Padding = new Padding(16);
        tabelaConfirmacao.RowCount = 5;
        tabelaConfirmacao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaConfirmacao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfirmacao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfirmacao.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        tabelaConfirmacao.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tabelaConfirmacao.Size = new Size(484, 198);
        tabelaConfirmacao.TabIndex = 0;
        //
        // lblOrientacao
        //
        lblOrientacao.AutoEllipsis = true;
        lblOrientacao.Dock = DockStyle.Fill;
        lblOrientacao.ForeColor = Color.DimGray;
        lblOrientacao.Location = new Point(19, 16);
        lblOrientacao.Name = "lblOrientacao";
        lblOrientacao.Size = new Size(446, 85);
        lblOrientacao.TabIndex = 0;
        lblOrientacao.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblSequenciaDfe
        //
        lblSequenciaDfe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblSequenciaDfe.AutoSize = true;
        lblSequenciaDfe.Location = new Point(19, 104);
        lblSequenciaDfe.Name = "lblSequenciaDfe";
        lblSequenciaDfe.Size = new Size(84, 15);
        lblSequenciaDfe.TabIndex = 1;
        lblSequenciaDfe.Text = "Sequência DFe";
        //
        // nudSequenciaDfe
        //
        nudSequenciaDfe.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        nudSequenciaDfe.Location = new Point(19, 123);
        nudSequenciaDfe.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
        nudSequenciaDfe.Name = "nudSequenciaDfe";
        nudSequenciaDfe.Size = new Size(446, 23);
        nudSequenciaDfe.TabIndex = 2;
        //
        // painelAcoes
        //
        painelAcoes.ColumnCount = 2;
        painelAcoes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        painelAcoes.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 172F));
        painelAcoes.Controls.Add(btnConfirmar, 1, 0);
        painelAcoes.Dock = DockStyle.Fill;
        painelAcoes.Name = "painelAcoes";
        painelAcoes.RowCount = 1;
        painelAcoes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        painelAcoes.TabIndex = 3;
        //
        // btnConfirmar
        //
        btnConfirmar.AutoSize = false;
        btnConfirmar.Dock = DockStyle.Fill;
        btnConfirmar.Margin = new Padding(3);
        btnConfirmar.Name = "btnConfirmar";
        btnConfirmar.Size = new Size(166, 28);
        btnConfirmar.TabIndex = 0;
        btnConfirmar.Text = "Confirmar Recebimento";
        btnConfirmar.UseVisualStyleBackColor = true;
        btnConfirmar.Click += btnConfirmar_Click;
        //
        // FormularioConfirmarAck
        //
        AcceptButton = btnConfirmar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(484, 198);
        Controls.Add(tabelaConfirmacao);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(500, 237);
        Name = "FormularioConfirmarAck";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Confirmar Recebimento";
        tabelaConfirmacao.ResumeLayout(false);
        tabelaConfirmacao.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSequenciaDfe).EndInit();
        painelAcoes.ResumeLayout(false);
        ResumeLayout(false);
    }
}
