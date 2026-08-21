#nullable enable

namespace SimuladorDFe;

partial class FormularioConfiguracao
{
    private System.ComponentModel.IContainer? components = null;
    private TableLayoutPanel tabelaConfiguracao = null!;
    private Label lblUrl = null!;
    private TextBox _txtUrl = null!;
    private Label lblTipoDocumento = null!;
    private ComboBox _cmbTipoDocumento = null!;
    private Label lblFluxoDocumento = null!;
    private ComboBox _cmbFluxoDocumento = null!;
    private Label lblSequencia = null!;
    private NumericUpDown _nudSequencia = null!;
    private Label lblPontoImpressao = null!;
    private TextBox _txtPontoImpressao = null!;
    private Label lblToken = null!;
    private TextBox _txtToken = null!;
    private CheckBox _chkMostrarToken = null!;
    private Label lblEndpoint = null!;
    private TextBox _txtEndpoint = null!;
    private Label _lblOrientacao = null!;
    private FlowLayoutPanel painelAcoes = null!;
    private Button btnSalvar = null!;
    private Button btnCancelar = null!;

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
        lblTipoDocumento = new Label();
        _cmbTipoDocumento = new ComboBox();
        lblFluxoDocumento = new Label();
        _cmbFluxoDocumento = new ComboBox();
        lblSequencia = new Label();
        _nudSequencia = new NumericUpDown();
        lblPontoImpressao = new Label();
        _txtPontoImpressao = new TextBox();
        lblToken = new Label();
        _txtToken = new TextBox();
        _chkMostrarToken = new CheckBox();
        lblEndpoint = new Label();
        _txtEndpoint = new TextBox();
        _lblOrientacao = new Label();
        painelAcoes = new FlowLayoutPanel();
        btnSalvar = new Button();
        btnCancelar = new Button();
        ((System.ComponentModel.ISupportInitialize)_nudSequencia).BeginInit();
        painelAcoes.SuspendLayout();
        tabelaConfiguracao.SuspendLayout();
        SuspendLayout();
        // 
        // tabelaConfiguracao
        // 
        tabelaConfiguracao.ColumnCount = 2;
        tabelaConfiguracao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tabelaConfiguracao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tabelaConfiguracao.Controls.Add(lblUrl, 0, 0);
        tabelaConfiguracao.Controls.Add(_txtUrl, 0, 1);
        tabelaConfiguracao.Controls.Add(lblTipoDocumento, 0, 2);
        tabelaConfiguracao.Controls.Add(_cmbTipoDocumento, 0, 3);
        tabelaConfiguracao.Controls.Add(lblFluxoDocumento, 1, 2);
        tabelaConfiguracao.Controls.Add(_cmbFluxoDocumento, 1, 3);
        tabelaConfiguracao.Controls.Add(lblSequencia, 0, 4);
        tabelaConfiguracao.Controls.Add(_nudSequencia, 0, 5);
        tabelaConfiguracao.Controls.Add(lblPontoImpressao, 1, 4);
        tabelaConfiguracao.Controls.Add(_txtPontoImpressao, 1, 5);
        tabelaConfiguracao.Controls.Add(lblToken, 0, 6);
        tabelaConfiguracao.Controls.Add(_txtToken, 0, 7);
        tabelaConfiguracao.Controls.Add(_chkMostrarToken, 1, 7);
        tabelaConfiguracao.Controls.Add(lblEndpoint, 0, 8);
        tabelaConfiguracao.Controls.Add(_txtEndpoint, 0, 9);
        tabelaConfiguracao.Controls.Add(_lblOrientacao, 0, 10);
        tabelaConfiguracao.Controls.Add(painelAcoes, 0, 12);
        tabelaConfiguracao.Dock = DockStyle.Fill;
        tabelaConfiguracao.Location = new Point(0, 0);
        tabelaConfiguracao.Name = "tabelaConfiguracao";
        tabelaConfiguracao.Padding = new Padding(16, 16, 16, 24);
        tabelaConfiguracao.RowCount = 13;
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        tabelaConfiguracao.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tabelaConfiguracao.SetColumnSpan(_txtUrl, 2);
        tabelaConfiguracao.SetColumnSpan(lblUrl, 2);
        tabelaConfiguracao.SetColumnSpan(lblToken, 2);
        tabelaConfiguracao.SetColumnSpan(lblEndpoint, 2);
        tabelaConfiguracao.SetColumnSpan(_txtEndpoint, 2);
        tabelaConfiguracao.SetColumnSpan(_lblOrientacao, 2);
        tabelaConfiguracao.SetColumnSpan(painelAcoes, 2);
        tabelaConfiguracao.Size = new Size(704, 440);
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
        _txtUrl.Size = new Size(666, 23);
        _txtUrl.TabIndex = 1;
        _txtUrl.TextChanged += txtUrl_TextChanged;
        // 
        // lblTipoDocumento
        // 
        lblTipoDocumento.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblTipoDocumento.AutoSize = true;
        lblTipoDocumento.Location = new Point(19, 69);
        lblTipoDocumento.Name = "lblTipoDocumento";
        lblTipoDocumento.Size = new Size(113, 15);
        lblTipoDocumento.TabIndex = 2;
        lblTipoDocumento.Text = "Tipo de Documento";
        // 
        // _cmbTipoDocumento
        // 
        _cmbTipoDocumento.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _cmbTipoDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbTipoDocumento.FormattingEnabled = true;
        _cmbTipoDocumento.Items.AddRange(new object[] { "NF-e (XML nfeProc)", "CT-e (XML cteProc ou cteSimpProc)", "NFS-e (JSON)", "MDF-e (XML mdfeProc de retorno)" });
        _cmbTipoDocumento.Location = new Point(19, 87);
        _cmbTipoDocumento.Name = "_cmbTipoDocumento";
        _cmbTipoDocumento.Size = new Size(327, 23);
        _cmbTipoDocumento.TabIndex = 3;
        _cmbTipoDocumento.SelectedIndexChanged += cmbTipoDocumento_SelectedIndexChanged;
        // 
        // lblFluxoDocumento
        // 
        lblFluxoDocumento.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblFluxoDocumento.AutoSize = true;
        lblFluxoDocumento.Location = new Point(352, 69);
        lblFluxoDocumento.Name = "lblFluxoDocumento";
        lblFluxoDocumento.Size = new Size(35, 15);
        lblFluxoDocumento.TabIndex = 4;
        lblFluxoDocumento.Text = "Fluxo";
        // 
        // _cmbFluxoDocumento
        // 
        _cmbFluxoDocumento.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _cmbFluxoDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbFluxoDocumento.FormattingEnabled = true;
        _cmbFluxoDocumento.Items.AddRange(new object[] { "Emissão", "Recepção" });
        _cmbFluxoDocumento.Location = new Point(352, 87);
        _cmbFluxoDocumento.Name = "_cmbFluxoDocumento";
        _cmbFluxoDocumento.Size = new Size(333, 23);
        _cmbFluxoDocumento.TabIndex = 5;
        _cmbFluxoDocumento.SelectedIndexChanged += cmbFluxoDocumento_SelectedIndexChanged;
        // 
        // lblSequencia
        // 
        lblSequencia.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblSequencia.AutoSize = true;
        lblSequencia.Location = new Point(19, 118);
        lblSequencia.Name = "lblSequencia";
        lblSequencia.Size = new Size(84, 15);
        lblSequencia.TabIndex = 6;
        lblSequencia.Text = "Sequência DFe";
        // 
        // _nudSequencia
        // 
        _nudSequencia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _nudSequencia.Location = new Point(19, 136);
        _nudSequencia.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
        _nudSequencia.Name = "_nudSequencia";
        _nudSequencia.Size = new Size(327, 23);
        _nudSequencia.TabIndex = 7;
        // 
        // lblPontoImpressao
        // 
        lblPontoImpressao.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblPontoImpressao.AutoSize = true;
        lblPontoImpressao.Location = new Point(352, 118);
        lblPontoImpressao.Name = "lblPontoImpressao";
        lblPontoImpressao.Size = new Size(112, 15);
        lblPontoImpressao.TabIndex = 8;
        lblPontoImpressao.Text = "Ponto de Impressão";
        // 
        // _txtPontoImpressao
        // 
        _txtPontoImpressao.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtPontoImpressao.Location = new Point(352, 136);
        _txtPontoImpressao.Name = "_txtPontoImpressao";
        _txtPontoImpressao.Size = new Size(333, 23);
        _txtPontoImpressao.TabIndex = 9;
        // 
        // lblToken
        // 
        lblToken.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblToken.AutoSize = true;
        lblToken.Location = new Point(19, 167);
        lblToken.Name = "lblToken";
        lblToken.Size = new Size(98, 15);
        lblToken.TabIndex = 10;
        lblToken.Text = "Token (Opcional)";
        // 
        // _txtToken
        // 
        _txtToken.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtToken.Location = new Point(19, 185);
        _txtToken.Name = "_txtToken";
        _txtToken.Size = new Size(327, 23);
        _txtToken.TabIndex = 11;
        _txtToken.UseSystemPasswordChar = true;
        // 
        // _chkMostrarToken
        // 
        _chkMostrarToken.Anchor = AnchorStyles.Left;
        _chkMostrarToken.AutoSize = true;
        _chkMostrarToken.Location = new Point(352, 187);
        _chkMostrarToken.Name = "_chkMostrarToken";
        _chkMostrarToken.Size = new Size(102, 19);
        _chkMostrarToken.TabIndex = 12;
        _chkMostrarToken.Text = "Mostrar Token";
        _chkMostrarToken.UseVisualStyleBackColor = true;
        _chkMostrarToken.CheckedChanged += chkMostrarToken_CheckedChanged;
        // 
        // lblEndpoint
        // 
        lblEndpoint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblEndpoint.AutoSize = true;
        lblEndpoint.Location = new Point(19, 216);
        lblEndpoint.Name = "lblEndpoint";
        lblEndpoint.Size = new Size(86, 15);
        lblEndpoint.TabIndex = 13;
        lblEndpoint.Text = "Rota Calculada";
        // 
        // _txtEndpoint
        // 
        _txtEndpoint.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtEndpoint.BackColor = SystemColors.Control;
        _txtEndpoint.Location = new Point(19, 234);
        _txtEndpoint.Name = "_txtEndpoint";
        _txtEndpoint.ReadOnly = true;
        _txtEndpoint.Size = new Size(666, 23);
        _txtEndpoint.TabIndex = 14;
        // 
        // _lblOrientacao
        // 
        _lblOrientacao.AutoEllipsis = true;
        _lblOrientacao.Dock = DockStyle.Fill;
        _lblOrientacao.ForeColor = Color.DimGray;
        _lblOrientacao.Location = new Point(19, 264);
        _lblOrientacao.Name = "_lblOrientacao";
        _lblOrientacao.Size = new Size(666, 84);
        _lblOrientacao.TabIndex = 15;
        _lblOrientacao.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // painelAcoes
        // 
        painelAcoes.AutoSize = true;
        painelAcoes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        painelAcoes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        painelAcoes.FlowDirection = FlowDirection.LeftToRight;
        painelAcoes.Location = new Point(497, 359);
        painelAcoes.Name = "painelAcoes";
        painelAcoes.Size = new Size(188, 28);
        painelAcoes.TabIndex = 16;
        painelAcoes.WrapContents = false;
        painelAcoes.Controls.Add(btnSalvar);
        painelAcoes.Controls.Add(btnCancelar);
        // 
        // btnSalvar
        // 
        btnSalvar.AutoSize = true;
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(88, 28);
        btnSalvar.TabIndex = 0;
        btnSalvar.Text = "Salvar";
        btnSalvar.UseVisualStyleBackColor = true;
        btnSalvar.Click += btnSalvar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.AutoSize = true;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(94, 28);
        btnCancelar.TabIndex = 1;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        // 
        // FormularioConfiguracao
        // 
        AcceptButton = btnSalvar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(704, 440);
        Controls.Add(tabelaConfiguracao);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(720, 479);
        Name = "FormularioConfiguracao";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Configurar API";
        ((System.ComponentModel.ISupportInitialize)_nudSequencia).EndInit();
        painelAcoes.ResumeLayout(false);
        painelAcoes.PerformLayout();
        tabelaConfiguracao.ResumeLayout(false);
        tabelaConfiguracao.PerformLayout();
        ResumeLayout(false);
    }
}
