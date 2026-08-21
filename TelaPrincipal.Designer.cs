#nullable enable

namespace SimuladorDFe;

partial class TelaPrincipal
{
    private System.ComponentModel.IContainer? components = null;
    private ToolStrip toolAcoes = null!;
    private ToolStripButton btnConfigurar = null!;
    private ToolStripButton btnAbrirArquivo = null!;
    private ToolStripButton btnGerarPrevia = null!;
    private ToolStripButton btnEnviar = null!;
    private ToolStripButton btnColetarPendentes = null!;
    private ToolStripButton btnConfirmarAck = null!;
    private ToolStripButton btnLimpar = null!;
    private TableLayoutPanel tabelaPrincipal = null!;
    private SplitContainer splitConteudo = null!;
    private TabControl tabDocumento = null!;
    private TabPage tabEdicaoCampos = null!;
    private TabPage tabConteudoDocumento = null!;
    private SplitContainer splitCampos = null!;
    private Label lblArvoreCampos = null!;
    private TextBox txtPesquisarTags = null!;
    private TreeView arvoreCampos = null!;
    private Panel painelEdicaoCampo = null!;
    private Label lblCaminhoCampo = null!;
    private TextBox txtCaminhoCampo = null!;
    private Label lblValorCampo = null!;
    private TextBox txtValorCampo = null!;
    private GroupBox grupoAtributos = null!;
    private FlowLayoutPanel painelAtributos = null!;
    private Label lblDocumento = null!;
    private RichTextBox txtDocumento = null!;
    private TabControl tabResultado = null!;
    private TabPage tabJson = null!;
    private TabPage tabRequisicao = null!;
    private TabPage tabResposta = null!;
    private TabPage tabHistorico = null!;
    private RichTextBox txtJson = null!;
    private RichTextBox txtRequisicao = null!;
    private RichTextBox txtResposta = null!;
    private RichTextBox txtHistorico = null!;
    private StatusStrip statusStrip = null!;
    private ToolStripStatusLabel lblStatus = null!;
    private OpenFileDialog openFileDialog = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        toolAcoes = new ToolStrip();
        btnConfigurar = new ToolStripButton();
        btnAbrirArquivo = new ToolStripButton();
        btnGerarPrevia = new ToolStripButton();
        btnEnviar = new ToolStripButton();
        btnColetarPendentes = new ToolStripButton();
        btnConfirmarAck = new ToolStripButton();
        btnLimpar = new ToolStripButton();
        tabelaPrincipal = new TableLayoutPanel();
        splitConteudo = new SplitContainer();
        tabDocumento = new TabControl();
        tabEdicaoCampos = new TabPage();
        splitCampos = new SplitContainer();
        arvoreCampos = new TreeView();
        txtPesquisarTags = new TextBox();
        lblArvoreCampos = new Label();
        painelEdicaoCampo = new Panel();
        grupoAtributos = new GroupBox();
        painelAtributos = new FlowLayoutPanel();
        txtValorCampo = new TextBox();
        lblValorCampo = new Label();
        txtCaminhoCampo = new TextBox();
        lblCaminhoCampo = new Label();
        tabConteudoDocumento = new TabPage();
        txtDocumento = new RichTextBox();
        lblDocumento = new Label();
        tabResultado = new TabControl();
        tabJson = new TabPage();
        txtJson = new RichTextBox();
        tabRequisicao = new TabPage();
        txtRequisicao = new RichTextBox();
        tabResposta = new TabPage();
        txtResposta = new RichTextBox();
        tabHistorico = new TabPage();
        txtHistorico = new RichTextBox();
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        openFileDialog = new OpenFileDialog();
        toolAcoes.SuspendLayout();
        tabelaPrincipal.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitConteudo).BeginInit();
        splitConteudo.Panel1.SuspendLayout();
        splitConteudo.Panel2.SuspendLayout();
        splitConteudo.SuspendLayout();
        tabDocumento.SuspendLayout();
        tabEdicaoCampos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitCampos).BeginInit();
        splitCampos.Panel1.SuspendLayout();
        splitCampos.Panel2.SuspendLayout();
        splitCampos.SuspendLayout();
        painelEdicaoCampo.SuspendLayout();
        tabConteudoDocumento.SuspendLayout();
        tabResultado.SuspendLayout();
        tabJson.SuspendLayout();
        tabRequisicao.SuspendLayout();
        tabResposta.SuspendLayout();
        tabHistorico.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        //
        // toolAcoes
        //
        toolAcoes.AutoSize = false;
        toolAcoes.GripStyle = ToolStripGripStyle.Hidden;
        toolAcoes.ImageScalingSize = new Size(20, 20);
        toolAcoes.Items.AddRange(new ToolStripItem[] { btnConfigurar, btnAbrirArquivo, btnGerarPrevia, btnEnviar, btnColetarPendentes, btnConfirmarAck, btnLimpar });
        toolAcoes.Location = new Point(0, 0);
        toolAcoes.Name = "toolAcoes";
        toolAcoes.Padding = new Padding(8, 4, 8, 4);
        toolAcoes.Size = new Size(1184, 42);
        toolAcoes.TabIndex = 0;
        //
        // btnConfigurar
        //
        btnConfigurar.AutoSize = false;
        btnConfigurar.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnConfigurar.Font = new Font("Segoe UI", 8.5F);
        btnConfigurar.Margin = new Padding(0, 0, 4, 0);
        btnConfigurar.Name = "btnConfigurar";
        btnConfigurar.Size = new Size(145, 32);
        btnConfigurar.Text = "Configurar API (F3)";
        btnConfigurar.ToolTipText = "Abrir a configuração da API (F3)";
        btnConfigurar.Click += btnConfigurar_Click;
        //
        // btnAbrirArquivo
        //
        btnAbrirArquivo.AutoSize = false;
        btnAbrirArquivo.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnAbrirArquivo.Font = new Font("Segoe UI", 8.5F);
        btnAbrirArquivo.Margin = new Padding(0, 0, 4, 0);
        btnAbrirArquivo.Name = "btnAbrirArquivo";
        btnAbrirArquivo.Size = new Size(150, 32);
        btnAbrirArquivo.Text = "Abrir Documento (F2)";
        btnAbrirArquivo.ToolTipText = "Selecionar o XML ou JSON a ser enviado (F2 ou Ctrl+O)";
        btnAbrirArquivo.Click += btnAbrirArquivo_Click;
        //
        // btnGerarPrevia
        //
        btnGerarPrevia.AutoSize = false;
        btnGerarPrevia.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnGerarPrevia.Font = new Font("Segoe UI", 8.5F);
        btnGerarPrevia.Margin = new Padding(0, 0, 4, 0);
        btnGerarPrevia.Name = "btnGerarPrevia";
        btnGerarPrevia.Size = new Size(130, 32);
        btnGerarPrevia.Text = "Gerar Prévia (F5)";
        btnGerarPrevia.ToolTipText = "Validar e montar o payload sem chamar a API (F5)";
        btnGerarPrevia.Click += btnGerarPrevia_Click;
        //
        // btnEnviar
        //
        btnEnviar.AutoSize = false;
        btnEnviar.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnEnviar.Font = new Font("Segoe UI", 8.5F);
        btnEnviar.Margin = new Padding(0, 0, 4, 0);
        btnEnviar.Name = "btnEnviar";
        btnEnviar.Size = new Size(135, 32);
        btnEnviar.Text = "Enviar Retorno (F10)";
        btnEnviar.ToolTipText = "Enviar o payload para a rota calculada a partir da URL base (F10)";
        btnEnviar.Click += btnEnviar_Click;
        //
        // btnColetarPendentes
        //
        btnColetarPendentes.AutoSize = false;
        btnColetarPendentes.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnColetarPendentes.Font = new Font("Segoe UI", 8.5F);
        btnColetarPendentes.Margin = new Padding(0, 0, 4, 0);
        btnColetarPendentes.Name = "btnColetarPendentes";
        btnColetarPendentes.Size = new Size(150, 32);
        btnColetarPendentes.Text = "Coletar Pendentes (F4)";
        btnColetarPendentes.ToolTipText = "Buscar os documentos de emissão pendentes para coleta (F4)";
        btnColetarPendentes.Click += btnColetarPendentes_Click;
        //
        // btnConfirmarAck
        //
        btnConfirmarAck.AutoSize = false;
        btnConfirmarAck.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnConfirmarAck.Font = new Font("Segoe UI", 8.5F);
        btnConfirmarAck.Margin = new Padding(0, 0, 4, 0);
        btnConfirmarAck.Name = "btnConfirmarAck";
        btnConfirmarAck.Size = new Size(140, 32);
        btnConfirmarAck.Text = "Confirmar ACK (F8)";
        btnConfirmarAck.ToolTipText = "Confirmar a coleta para a sequência DFe informada (F8)";
        btnConfirmarAck.Click += btnConfirmarAck_Click;
        //
        // btnLimpar
        //
        btnLimpar.AutoSize = false;
        btnLimpar.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnLimpar.Font = new Font("Segoe UI", 8.5F);
        btnLimpar.Name = "btnLimpar";
        btnLimpar.Size = new Size(110, 32);
        btnLimpar.Text = "Limpar (F12)";
        btnLimpar.ToolTipText = "Limpar documento e resultados, mantendo URL e token (F12)";
        btnLimpar.Click += btnLimpar_Click;
        //
        // tabelaPrincipal
        //
        tabelaPrincipal.ColumnCount = 1;
        tabelaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tabelaPrincipal.Controls.Add(splitConteudo, 0, 0);
        tabelaPrincipal.Dock = DockStyle.Fill;
        tabelaPrincipal.Location = new Point(0, 42);
        tabelaPrincipal.Name = "tabelaPrincipal";
        tabelaPrincipal.Padding = new Padding(12, 8, 12, 8);
        tabelaPrincipal.RowCount = 1;
        tabelaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaPrincipal.Size = new Size(1184, 720);
        tabelaPrincipal.TabIndex = 1;
        //
        // splitConteudo
        //
        splitConteudo.Dock = DockStyle.Fill;
        splitConteudo.Location = new Point(15, 11);
        splitConteudo.Name = "splitConteudo";
        splitConteudo.Orientation = Orientation.Horizontal;
        //
        // splitConteudo.Panel1
        //
        splitConteudo.Panel1.Controls.Add(tabDocumento);
        //
        // splitConteudo.Panel2
        //
        splitConteudo.Panel2.Controls.Add(tabResultado);
        splitConteudo.Size = new Size(1154, 525);
        splitConteudo.SplitterDistance = 422;
        splitConteudo.TabIndex = 1;
        //
        // tabDocumento
        //
        tabDocumento.Controls.Add(tabEdicaoCampos);
        tabDocumento.Controls.Add(tabConteudoDocumento);
        tabDocumento.Dock = DockStyle.Fill;
        tabDocumento.Location = new Point(0, 0);
        tabDocumento.Name = "tabDocumento";
        tabDocumento.SelectedIndex = 0;
        tabDocumento.Size = new Size(1154, 422);
        tabDocumento.TabIndex = 0;
        //
        // tabEdicaoCampos
        //
        tabEdicaoCampos.Controls.Add(splitCampos);
        tabEdicaoCampos.Location = new Point(4, 24);
        tabEdicaoCampos.Name = "tabEdicaoCampos";
        tabEdicaoCampos.Padding = new Padding(3);
        tabEdicaoCampos.Size = new Size(1146, 394);
        tabEdicaoCampos.TabIndex = 0;
        tabEdicaoCampos.Text = "Editar Valores";
        tabEdicaoCampos.UseVisualStyleBackColor = true;
        //
        // splitCampos
        //
        splitCampos.Dock = DockStyle.Fill;
        splitCampos.FixedPanel = FixedPanel.Panel2;
        splitCampos.Location = new Point(3, 3);
        splitCampos.Name = "splitCampos";
        splitCampos.Orientation = Orientation.Horizontal;
        //
        // splitCampos.Panel1
        //
        splitCampos.Panel1.Controls.Add(arvoreCampos);
        splitCampos.Panel1.Controls.Add(txtPesquisarTags);
        splitCampos.Panel1.Controls.Add(lblArvoreCampos);
        splitCampos.Panel1MinSize = 140;
        //
        // splitCampos.Panel2
        //
        splitCampos.Panel2.Controls.Add(painelEdicaoCampo);
        splitCampos.Panel2MinSize = 185;
        splitCampos.Size = new Size(1140, 388);
        splitCampos.SplitterDistance = 195;
        splitCampos.TabIndex = 0;
        //
        // arvoreCampos
        //
        arvoreCampos.AllowDrop = true;
        arvoreCampos.Dock = DockStyle.Fill;
        arvoreCampos.HideSelection = false;
        arvoreCampos.Location = new Point(0, 47);
        arvoreCampos.Name = "arvoreCampos";
        arvoreCampos.Size = new Size(1140, 148);
        arvoreCampos.TabIndex = 2;
        arvoreCampos.AfterSelect += arvoreCampos_AfterSelect;
        arvoreCampos.DragDrop += txtDocumento_DragDrop;
        arvoreCampos.DragEnter += txtDocumento_DragEnter;
        //
        // txtPesquisarTags
        //
        txtPesquisarTags.Dock = DockStyle.Top;
        txtPesquisarTags.Location = new Point(0, 24);
        txtPesquisarTags.Name = "txtPesquisarTags";
        txtPesquisarTags.PlaceholderText = "Pesquisar Tag, Valor ou Atributo";
        txtPesquisarTags.Size = new Size(1140, 23);
        txtPesquisarTags.TabIndex = 1;
        txtPesquisarTags.TextChanged += txtPesquisarTags_TextChanged;
        //
        // lblArvoreCampos
        //
        lblArvoreCampos.Dock = DockStyle.Top;
        lblArvoreCampos.Font = new Font("Segoe UI", 9F);
        lblArvoreCampos.Location = new Point(0, 0);
        lblArvoreCampos.Name = "lblArvoreCampos";
        lblArvoreCampos.Padding = new Padding(4, 4, 0, 0);
        lblArvoreCampos.Size = new Size(1140, 24);
        lblArvoreCampos.TabIndex = 0;
        lblArvoreCampos.Text = "Campos do XML";
        //
        // painelEdicaoCampo
        //
        painelEdicaoCampo.AllowDrop = true;
        painelEdicaoCampo.Controls.Add(grupoAtributos);
        painelEdicaoCampo.Controls.Add(txtValorCampo);
        painelEdicaoCampo.Controls.Add(lblValorCampo);
        painelEdicaoCampo.Controls.Add(txtCaminhoCampo);
        painelEdicaoCampo.Controls.Add(lblCaminhoCampo);
        painelEdicaoCampo.Dock = DockStyle.Fill;
        painelEdicaoCampo.Location = new Point(0, 0);
        painelEdicaoCampo.Name = "painelEdicaoCampo";
        painelEdicaoCampo.Size = new Size(1140, 189);
        painelEdicaoCampo.TabIndex = 0;
        painelEdicaoCampo.DragDrop += txtDocumento_DragDrop;
        painelEdicaoCampo.DragEnter += txtDocumento_DragEnter;
        // grupoAtributos
        //
        grupoAtributos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grupoAtributos.Controls.Add(painelAtributos);
        grupoAtributos.Location = new Point(4, 96);
        grupoAtributos.Name = "grupoAtributos";
        grupoAtributos.Padding = new Padding(8, 4, 8, 6);
        grupoAtributos.Size = new Size(1132, 79);
        grupoAtributos.TabIndex = 4;
        grupoAtributos.TabStop = false;
        grupoAtributos.Text = "Atributos";
        //
        // painelAtributos
        //
        painelAtributos.AutoScroll = true;
        painelAtributos.Dock = DockStyle.Fill;
        painelAtributos.FlowDirection = FlowDirection.LeftToRight;
        painelAtributos.Location = new Point(8, 4);
        painelAtributos.Name = "painelAtributos";
        painelAtributos.Size = new Size(1116, 69);
        painelAtributos.TabIndex = 0;
        painelAtributos.WrapContents = true;
        //
        // txtValorCampo
        //
        txtValorCampo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtValorCampo.Location = new Point(4, 68);
        txtValorCampo.Name = "txtValorCampo";
        txtValorCampo.Size = new Size(1132, 23);
        txtValorCampo.TabIndex = 3;
        txtValorCampo.TextChanged += txtValorCampo_TextChanged;
        //
        // lblValorCampo
        //
        lblValorCampo.AutoSize = true;
        lblValorCampo.Location = new Point(4, 50);
        lblValorCampo.Name = "lblValorCampo";
        lblValorCampo.Size = new Size(33, 15);
        lblValorCampo.TabIndex = 2;
        lblValorCampo.Text = "Valor";
        //
        // txtCaminhoCampo
        //
        txtCaminhoCampo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCaminhoCampo.Location = new Point(4, 22);
        txtCaminhoCampo.Name = "txtCaminhoCampo";
        txtCaminhoCampo.ReadOnly = true;
        txtCaminhoCampo.Size = new Size(1132, 23);
        txtCaminhoCampo.TabIndex = 1;
        //
        // lblCaminhoCampo
        //
        lblCaminhoCampo.AutoSize = true;
        lblCaminhoCampo.Location = new Point(4, 4);
        lblCaminhoCampo.Name = "lblCaminhoCampo";
        lblCaminhoCampo.Size = new Size(56, 15);
        lblCaminhoCampo.TabIndex = 0;
        lblCaminhoCampo.Text = "Caminho";
        //
        // tabConteudoDocumento
        //
        tabConteudoDocumento.Controls.Add(txtDocumento);
        tabConteudoDocumento.Controls.Add(lblDocumento);
        tabConteudoDocumento.Location = new Point(4, 24);
        tabConteudoDocumento.Name = "tabConteudoDocumento";
        tabConteudoDocumento.Padding = new Padding(3);
        tabConteudoDocumento.Size = new Size(1146, 422);
        tabConteudoDocumento.TabIndex = 1;
        tabConteudoDocumento.Text = "XML";
        tabConteudoDocumento.UseVisualStyleBackColor = true;
        //
        // txtDocumento
        //
        txtDocumento.AllowDrop = true;
        txtDocumento.BackColor = Color.White;
        txtDocumento.Dock = DockStyle.Fill;
        txtDocumento.Font = new Font("Consolas", 9F);
        txtDocumento.Location = new Point(3, 27);
        txtDocumento.Name = "txtDocumento";
        txtDocumento.Size = new Size(1140, 392);
        txtDocumento.TabIndex = 0;
        txtDocumento.Text = "";
        txtDocumento.DragDrop += txtDocumento_DragDrop;
        txtDocumento.DragEnter += txtDocumento_DragEnter;
        txtDocumento.TextChanged += txtDocumento_TextChanged;
        //
        // lblDocumento
        //
        lblDocumento.Dock = DockStyle.Top;
        lblDocumento.Font = new Font("Segoe UI", 9F);
        lblDocumento.Location = new Point(3, 3);
        lblDocumento.Name = "lblDocumento";
        lblDocumento.Padding = new Padding(4, 4, 0, 0);
        lblDocumento.Size = new Size(1140, 24);
        lblDocumento.TabIndex = 0;
        lblDocumento.Text = "XML Editável";
        //
        // tabResultado
        //
        tabResultado.Controls.Add(tabJson);
        tabResultado.Controls.Add(tabRequisicao);
        tabResultado.Controls.Add(tabResposta);
        tabResultado.Controls.Add(tabHistorico);
        tabResultado.Dock = DockStyle.Fill;
        tabResultado.Location = new Point(0, 0);
        tabResultado.Name = "tabResultado";
        tabResultado.SelectedIndex = 0;
        tabResultado.Size = new Size(1154, 99);
        tabResultado.TabIndex = 0;
        //
        // tabJson
        //
        tabJson.Controls.Add(txtJson);
        tabJson.Location = new Point(4, 24);
        tabJson.Name = "tabJson";
        tabJson.Padding = new Padding(3);
        tabJson.Size = new Size(1146, 71);
        tabJson.TabIndex = 0;
        tabJson.Text = "JSON Preparado";
        tabJson.UseVisualStyleBackColor = true;
        //
        // txtJson
        //
        txtJson.BackColor = Color.White;
        txtJson.Dock = DockStyle.Fill;
        txtJson.Font = new Font("Consolas", 9F);
        txtJson.Location = new Point(3, 3);
        txtJson.Name = "txtJson";
        txtJson.ReadOnly = true;
        txtJson.Size = new Size(1140, 65);
        txtJson.TabIndex = 0;
        txtJson.Text = "";
        txtJson.WordWrap = false;
        //
        // tabRequisicao
        //
        tabRequisicao.Controls.Add(txtRequisicao);
        tabRequisicao.Location = new Point(4, 24);
        tabRequisicao.Name = "tabRequisicao";
        tabRequisicao.Padding = new Padding(3);
        tabRequisicao.Size = new Size(1146, 71);
        tabRequisicao.TabIndex = 1;
        tabRequisicao.Text = "Corpo da Requisição";
        tabRequisicao.UseVisualStyleBackColor = true;
        //
        // txtRequisicao
        //
        txtRequisicao.BackColor = Color.White;
        txtRequisicao.Dock = DockStyle.Fill;
        txtRequisicao.Font = new Font("Consolas", 9F);
        txtRequisicao.Location = new Point(3, 3);
        txtRequisicao.Name = "txtRequisicao";
        txtRequisicao.ReadOnly = true;
        txtRequisicao.Size = new Size(1140, 65);
        txtRequisicao.TabIndex = 0;
        txtRequisicao.Text = "";
        txtRequisicao.WordWrap = false;
        //
        // tabResposta
        //
        tabResposta.Controls.Add(txtResposta);
        tabResposta.Location = new Point(4, 24);
        tabResposta.Name = "tabResposta";
        tabResposta.Padding = new Padding(3);
        tabResposta.Size = new Size(1146, 71);
        tabResposta.TabIndex = 2;
        tabResposta.Text = "Resposta da API";
        tabResposta.UseVisualStyleBackColor = true;
        //
        // txtResposta
        //
        txtResposta.BackColor = Color.White;
        txtResposta.Dock = DockStyle.Fill;
        txtResposta.Font = new Font("Consolas", 9F);
        txtResposta.Location = new Point(3, 3);
        txtResposta.Name = "txtResposta";
        txtResposta.ReadOnly = true;
        txtResposta.Size = new Size(1140, 65);
        txtResposta.TabIndex = 0;
        txtResposta.Text = "";
        txtResposta.WordWrap = false;
        //
        // tabHistorico
        //
        tabHistorico.Controls.Add(txtHistorico);
        tabHistorico.Location = new Point(4, 24);
        tabHistorico.Name = "tabHistorico";
        tabHistorico.Padding = new Padding(3);
        tabHistorico.Size = new Size(1146, 71);
        tabHistorico.TabIndex = 3;
        tabHistorico.Text = "Histórico da Sessão";
        tabHistorico.UseVisualStyleBackColor = true;
        //
        // txtHistorico
        //
        txtHistorico.BackColor = Color.White;
        txtHistorico.Dock = DockStyle.Fill;
        txtHistorico.Font = new Font("Consolas", 9F);
        txtHistorico.Location = new Point(3, 3);
        txtHistorico.Name = "txtHistorico";
        txtHistorico.ReadOnly = true;
        txtHistorico.Size = new Size(1140, 65);
        txtHistorico.TabIndex = 0;
        txtHistorico.Text = "";
        txtHistorico.WordWrap = false;
        //
        // statusStrip
        //
        statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
        statusStrip.Location = new Point(0, 762);
        statusStrip.Name = "statusStrip";
        statusStrip.Padding = new Padding(12, 0, 1, 0);
        statusStrip.Size = new Size(1184, 22);
        statusStrip.SizingGrip = false;
        statusStrip.TabIndex = 2;
        //
        // lblStatus
        //
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(1171, 17);
        lblStatus.Spring = true;
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        //
        // openFileDialog
        //
        openFileDialog.Title = "Selecionar Documento Fiscal";
        //
        // TelaPrincipal
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1184, 784);
        Controls.Add(tabelaPrincipal);
        Controls.Add(statusStrip);
        Controls.Add(toolAcoes);
        KeyPreview = true;
        MinimumSize = new Size(1024, 700);
        Name = "TelaPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Simulador de DFe — Recepção de Documentos";
        KeyDown += TelaPrincipal_KeyDown;
        toolAcoes.ResumeLayout(false);
        toolAcoes.PerformLayout();
        tabelaPrincipal.ResumeLayout(false);
        splitConteudo.Panel1.ResumeLayout(false);
        splitConteudo.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitConteudo).EndInit();
        splitConteudo.ResumeLayout(false);
        tabDocumento.ResumeLayout(false);
        tabEdicaoCampos.ResumeLayout(false);
        splitCampos.Panel1.ResumeLayout(false);
        splitCampos.Panel1.PerformLayout();
        splitCampos.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitCampos).EndInit();
        splitCampos.ResumeLayout(false);
        painelEdicaoCampo.ResumeLayout(false);
        painelEdicaoCampo.PerformLayout();
        tabConteudoDocumento.ResumeLayout(false);
        tabResultado.ResumeLayout(false);
        tabJson.ResumeLayout(false);
        tabRequisicao.ResumeLayout(false);
        tabResposta.ResumeLayout(false);
        tabHistorico.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
