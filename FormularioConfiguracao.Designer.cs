#nullable enable

namespace SimuladorDFe;

partial class FormularioConfiguracao
{
    private System.ComponentModel.IContainer? components = null;
    private TableLayoutPanel tabelaConfiguracao = null!;
    private Label lblUrl = null!;
    private TextBox _txtUrl = null!;
    private Label lblToken = null!;
    private TextBox _txtToken = null!;
    private CheckBox _chkMostrarToken = null!;
    private Label lblOrientacao = null!;
    private TableLayoutPanel painelAcoes = null!;
    private Button btnSalvar = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabelaConfiguracao = new TableLayoutPanel();
        lblUrl = new Label();
        _txtUrl = new TextBox();
        lblToken = new Label();
        _txtToken = new TextBox();
        _chkMostrarToken = new CheckBox();
        lblOrientacao = new Label();
        painelAcoes = new TableLayoutPanel();
        btnSalvar = new Button();
        tabelaConfiguracao.SuspendLayout();
        painelAcoes.SuspendLayout();
        SuspendLayout();
        //
        // tabelaConfiguracao
        //
        tabelaConfiguracao.ColumnCount = 2;
        tabelaConfiguracao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
        tabelaConfiguracao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        tabelaConfiguracao.Controls.Add(lblUrl, 0, 0);
        tabelaConfiguracao.Controls.Add(_txtUrl, 0, 1);
        tabelaConfiguracao.Controls.Add(lblToken, 0, 2);
        tabelaConfiguracao.Controls.Add(_txtToken, 0, 3);
        tabelaConfiguracao.Controls.Add(_chkMostrarToken, 1, 3);
        tabelaConfiguracao.Controls.Add(lblOrientacao, 0, 4);
        tabelaConfiguracao.Controls.Add(painelAcoes, 0, 6);
        tabelaConfiguracao.Dock = DockStyle.Fill;
        tabelaConfiguracao.Location = new Point(0, 0);
        tabelaConfiguracao.Name = "tabelaConfiguracao";
        tabelaConfiguracao.Padding = new Padding(16);
        tabelaConfiguracao.RowCount = 7;
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tabelaConfiguracao.SetColumnSpan(lblUrl, 2);
        tabelaConfiguracao.SetColumnSpan(_txtUrl, 2);
        tabelaConfiguracao.SetColumnSpan(lblToken, 2);
        tabelaConfiguracao.SetColumnSpan(lblOrientacao, 2);
        tabelaConfiguracao.SetColumnSpan(painelAcoes, 2);
        tabelaConfiguracao.Size = new Size(604, 226);
        tabelaConfiguracao.TabIndex = 0;
        //
        // lblUrl
        //
        lblUrl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblUrl.AutoSize = true;
        lblUrl.Location = new Point(19, 20);
        lblUrl.Name = "lblUrl";
        lblUrl.Size = new Size(92, 15);
        lblUrl.TabIndex = 0;
        lblUrl.Text = "URL Base da API";
        //
        // _txtUrl
        //
        _txtUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtUrl.Location = new Point(19, 38);
        _txtUrl.Name = "_txtUrl";
        _txtUrl.PlaceholderText = "https://servidor/IntegracaoDFe";
        _txtUrl.Size = new Size(566, 23);
        _txtUrl.TabIndex = 1;
        //
        // lblToken
        //
        lblToken.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblToken.AutoSize = true;
        lblToken.Location = new Point(19, 69);
        lblToken.Name = "lblToken";
        lblToken.Size = new Size(98, 15);
        lblToken.TabIndex = 2;
        lblToken.Text = "Token (Opcional)";
        //
        // _txtToken
        //
        _txtToken.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtToken.Location = new Point(19, 87);
        _txtToken.Name = "_txtToken";
        _txtToken.Size = new Size(401, 23);
        _txtToken.TabIndex = 3;
        _txtToken.UseSystemPasswordChar = true;
        //
        // _chkMostrarToken
        //
        _chkMostrarToken.Anchor = AnchorStyles.Left;
        _chkMostrarToken.AutoSize = true;
        _chkMostrarToken.Location = new Point(426, 89);
        _chkMostrarToken.Name = "_chkMostrarToken";
        _chkMostrarToken.Size = new Size(102, 19);
        _chkMostrarToken.TabIndex = 4;
        _chkMostrarToken.Text = "Mostrar Token";
        _chkMostrarToken.UseVisualStyleBackColor = true;
        _chkMostrarToken.CheckedChanged += chkMostrarToken_CheckedChanged;
        //
        // lblOrientacao
        //
        lblOrientacao.AutoEllipsis = true;
        lblOrientacao.Dock = DockStyle.Fill;
        lblOrientacao.ForeColor = Color.DimGray;
        lblOrientacao.Location = new Point(19, 116);
        lblOrientacao.Name = "lblOrientacao";
        lblOrientacao.Size = new Size(566, 52);
        lblOrientacao.TabIndex = 5;
        lblOrientacao.Text = "A configuração é compartilhada pelos processos de retorno, coleta e ACK. Escolha o documento e o fluxo dentro de cada aba.";
        lblOrientacao.TextAlign = ContentAlignment.MiddleLeft;
        //
        // painelAcoes
        //
        painelAcoes.ColumnCount = 2;
        painelAcoes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        painelAcoes.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 94F));
        painelAcoes.Controls.Add(btnSalvar, 1, 0);
        painelAcoes.Dock = DockStyle.Fill;
        painelAcoes.Name = "painelAcoes";
        painelAcoes.RowCount = 1;
        painelAcoes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        painelAcoes.TabIndex = 6;
        //
        // btnSalvar
        //
        btnSalvar.AutoSize = false;
        btnSalvar.Dock = DockStyle.Fill;
        btnSalvar.Margin = new Padding(3);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(88, 28);
        btnSalvar.TabIndex = 0;
        btnSalvar.Text = "Salvar";
        btnSalvar.UseVisualStyleBackColor = true;
        btnSalvar.Click += btnSalvar_Click;
        // FormularioConfiguracao
        //
        AcceptButton = btnSalvar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(604, 226);
        Controls.Add(tabelaConfiguracao);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(620, 265);
        Name = "FormularioConfiguracao";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Configurar API";
        tabelaConfiguracao.ResumeLayout(false);
        tabelaConfiguracao.PerformLayout();
        painelAcoes.ResumeLayout(false);
        painelAcoes.PerformLayout();
        ResumeLayout(false);
    }
}
