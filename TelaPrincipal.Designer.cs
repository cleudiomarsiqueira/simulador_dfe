#nullable enable

namespace SimuladorDFe;

partial class TelaPrincipal
{
    private System.ComponentModel.IContainer? components = null;
    private MenuStrip menuPrincipal = null!;
    private ToolStripMenuItem menuArquivo = null!;
    private ToolStripMenuItem menuArquivoAbrir = null!;
    private ToolStripMenuItem menuArquivoLimpar = null!;
    private ToolStripMenuItem menuArquivoSair = null!;
    private ToolStripMenuItem menuEditar = null!;
    private ToolStripMenuItem menuEditarValores = null!;
    private ToolStripMenuItem menuEditarDocumento = null!;
    private ToolStripMenuItem menuFerramentas = null!;
    private ToolStripMenuItem menuFerramentasConfigurar = null!;
    private ToolStripMenuItem menuAjuda = null!;
    private ToolStripMenuItem menuAjudaSobre = null!;
    private ToolStripButton btnAbrirArquivo = null!;
    private ToolStripButton btnEnviar = null!;
    private ToolStripButton btnColetarPendentes = null!;
    private ToolStripButton btnConfirmarAck = null!;
    private ToolStripButton btnLimpar = null!;
    private ToolStripButton btnSalvarXmlAlterado = null!;
    private ToolStripButton btnLimparColeta = null!;
    private TableLayoutPanel tabelaPrincipal = null!;
    private TabControl tabProcessos = null!;
    private TabPage tabRetorno = null!;
    private TabPage tabColeta = null!;
    private TableLayoutPanel tabelaRetorno = null!;
    private TableLayoutPanel tabelaParametrosRetorno = null!;
    private Label lblTipoDocumentoRetorno = null!;
    private ComboBox cmbTipoDocumentoRetorno = null!;
    private Label lblFluxoRetorno = null!;
    private ComboBox cmbFluxoRetorno = null!;
    private Label lblSequenciaRetorno = null!;
    private NumericUpDown nudSequenciaRetorno = null!;
    private Label lblPontoImpressaoRetorno = null!;
    private TextBox txtPontoImpressaoRetorno = null!;
    private ToolStrip toolRetorno = null!;
    private TableLayoutPanel tabelaColeta = null!;
    private TableLayoutPanel tabelaParametrosColeta = null!;
    private Label lblTipoDocumentoColeta = null!;
    private ComboBox cmbTipoDocumentoColeta = null!;
    private Label lblOrientacaoColeta = null!;
    private ToolStrip toolColeta = null!;
    private TabControl tabResultadoColeta = null!;
    private TabPage tabRequisicaoColeta = null!;
    private RichTextBox txtRequisicaoColeta = null!;
    private TabPage tabRespostaColeta = null!;
    private RichTextBox txtRespostaColeta = null!;
    private TabPage tabHistoricoColeta = null!;
    private RichTextBox txtHistoricoColeta = null!;
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
    private ToolStripStatusLabel lblIndicadorStatus = null!;
    private OpenFileDialog openFileDialog = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        menuPrincipal = new MenuStrip();
        menuArquivo = new ToolStripMenuItem();
        menuArquivoAbrir = new ToolStripMenuItem();
        menuArquivoLimpar = new ToolStripMenuItem();
        menuArquivoSair = new ToolStripMenuItem();
        menuEditar = new ToolStripMenuItem();
        menuEditarValores = new ToolStripMenuItem();
        menuEditarDocumento = new ToolStripMenuItem();
        menuFerramentas = new ToolStripMenuItem();
        menuFerramentasConfigurar = new ToolStripMenuItem();
        menuAjuda = new ToolStripMenuItem();
        menuAjudaSobre = new ToolStripMenuItem();
        btnAbrirArquivo = new ToolStripButton();
        btnEnviar = new ToolStripButton();
        btnColetarPendentes = new ToolStripButton();
        btnConfirmarAck = new ToolStripButton();
        btnLimpar = new ToolStripButton();
        btnSalvarXmlAlterado = new ToolStripButton();
        btnLimparColeta = new ToolStripButton();
        tabelaPrincipal = new TableLayoutPanel();
        tabProcessos = new TabControl();
        tabRetorno = new TabPage();
        tabelaRetorno = new TableLayoutPanel();
        tabelaParametrosRetorno = new TableLayoutPanel();
        lblTipoDocumentoRetorno = new Label();
        cmbTipoDocumentoRetorno = new ComboBox();
        lblFluxoRetorno = new Label();
        cmbFluxoRetorno = new ComboBox();
        lblSequenciaRetorno = new Label();
        nudSequenciaRetorno = new NumericUpDown();
        lblPontoImpressaoRetorno = new Label();
        txtPontoImpressaoRetorno = new TextBox();
        toolRetorno = new ToolStrip();
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
        tabColeta = new TabPage();
        tabelaColeta = new TableLayoutPanel();
        tabelaParametrosColeta = new TableLayoutPanel();
        lblTipoDocumentoColeta = new Label();
        cmbTipoDocumentoColeta = new ComboBox();
        lblOrientacaoColeta = new Label();
        toolColeta = new ToolStrip();
        tabResultadoColeta = new TabControl();
        tabRequisicaoColeta = new TabPage();
        txtRequisicaoColeta = new RichTextBox();
        tabRespostaColeta = new TabPage();
        txtRespostaColeta = new RichTextBox();
        tabHistoricoColeta = new TabPage();
        txtHistoricoColeta = new RichTextBox();
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        lblIndicadorStatus = new ToolStripStatusLabel();
        openFileDialog = new OpenFileDialog();
        menuPrincipal.SuspendLayout();
        tabelaPrincipal.SuspendLayout();
        tabProcessos.SuspendLayout();
        tabRetorno.SuspendLayout();
        tabelaRetorno.SuspendLayout();
        tabelaParametrosRetorno.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSequenciaRetorno).BeginInit();
        toolRetorno.SuspendLayout();
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
        grupoAtributos.SuspendLayout();
        tabConteudoDocumento.SuspendLayout();
        tabResultado.SuspendLayout();
        tabJson.SuspendLayout();
        tabRequisicao.SuspendLayout();
        tabResposta.SuspendLayout();
        tabHistorico.SuspendLayout();
        tabColeta.SuspendLayout();
        tabelaColeta.SuspendLayout();
        tabelaParametrosColeta.SuspendLayout();
        toolColeta.SuspendLayout();
        tabResultadoColeta.SuspendLayout();
        tabRequisicaoColeta.SuspendLayout();
        tabRespostaColeta.SuspendLayout();
        tabHistoricoColeta.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        //
        // menuPrincipal
        //
        menuPrincipal.Items.AddRange(new ToolStripItem[] { menuArquivo, menuEditar, menuFerramentas, menuAjuda });
        menuPrincipal.Location = new Point(0, 0);
        menuPrincipal.Name = "menuPrincipal";
        menuPrincipal.Size = new Size(1184, 24);
        menuPrincipal.TabIndex = 0;
        menuPrincipal.Text = "menuPrincipal";
        //
        // menuArquivo
        //
        menuArquivo.DropDownItems.AddRange(new ToolStripItem[] { menuArquivoAbrir, menuArquivoLimpar, menuArquivoSair });
        menuArquivo.Name = "menuArquivo";
        menuArquivo.Size = new Size(61, 20);
        menuArquivo.Text = "Arquivo";
        //
        // menuArquivoAbrir
        //
        menuArquivoAbrir.Name = "menuArquivoAbrir";
        menuArquivoAbrir.ShortcutKeyDisplayString = "F2";
        menuArquivoAbrir.Size = new Size(212, 22);
        menuArquivoAbrir.Text = "Abrir Documento";
        menuArquivoAbrir.Click += btnAbrirArquivo_Click;
        //
        // menuArquivoLimpar
        //
        menuArquivoLimpar.Name = "menuArquivoLimpar";
        menuArquivoLimpar.ShortcutKeyDisplayString = "F12";
        menuArquivoLimpar.Size = new Size(212, 22);
        menuArquivoLimpar.Text = "Limpar Dados da Aba";
        menuArquivoLimpar.Click += menuArquivoLimpar_Click;
        //
        // menuArquivoSair
        //
        menuArquivoSair.Name = "menuArquivoSair";
        menuArquivoSair.Size = new Size(212, 22);
        menuArquivoSair.Text = "Sair";
        menuArquivoSair.Click += menuArquivoSair_Click;
        //
        // menuEditar
        //
        menuEditar.DropDownItems.AddRange(new ToolStripItem[] { menuEditarValores, menuEditarDocumento });
        menuEditar.Name = "menuEditar";
        menuEditar.Size = new Size(49, 20);
        menuEditar.Text = "Editar";
        //
        // menuEditarValores
        //
        menuEditarValores.Name = "menuEditarValores";
        menuEditarValores.Size = new Size(183, 22);
        menuEditarValores.Text = "Editar Valores";
        menuEditarValores.Click += menuEditarValores_Click;
        //
        // menuEditarDocumento
        //
        menuEditarDocumento.Name = "menuEditarDocumento";
        menuEditarDocumento.Size = new Size(183, 22);
        menuEditarDocumento.Text = "Visualizar XML/JSON";
        menuEditarDocumento.Click += menuEditarDocumento_Click;
        //
        // menuFerramentas
        //
        menuFerramentas.DropDownItems.AddRange(new ToolStripItem[] { menuFerramentasConfigurar });
        menuFerramentas.Name = "menuFerramentas";
        menuFerramentas.Size = new Size(84, 20);
        menuFerramentas.Text = "Ferramentas";
        //
        // menuFerramentasConfigurar
        //
        menuFerramentasConfigurar.Name = "menuFerramentasConfigurar";
        menuFerramentasConfigurar.ShortcutKeyDisplayString = "F3";
        menuFerramentasConfigurar.Size = new Size(171, 22);
        menuFerramentasConfigurar.Text = "Configurar API";
        menuFerramentasConfigurar.Click += menuFerramentasConfigurar_Click;
        //
        // menuAjuda
        //
        menuAjuda.DropDownItems.AddRange(new ToolStripItem[] { menuAjudaSobre });
        menuAjuda.Name = "menuAjuda";
        menuAjuda.Size = new Size(50, 20);
        menuAjuda.Text = "Ajuda";
        //
        // menuAjudaSobre
        //
        menuAjudaSobre.Name = "menuAjudaSobre";
        menuAjudaSobre.Size = new Size(194, 22);
        menuAjudaSobre.Text = "Sobre o Simulador DFe";
        menuAjudaSobre.Click += menuAjudaSobre_Click;
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
        // btnEnviar
        //
        btnEnviar.AutoSize = false;
        btnEnviar.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnEnviar.Font = new Font("Segoe UI", 8.5F);
        btnEnviar.Margin = new Padding(0, 0, 4, 0);
        btnEnviar.Name = "btnEnviar";
        btnEnviar.Size = new Size(180, 32);
        btnEnviar.Text = "Simular Recepção (F10)";
        btnEnviar.ToolTipText = "Simular a recepção conforme a origem do documento selecionada (F10)";
        btnEnviar.Click += btnEnviar_Click;
        //
        // btnColetarPendentes
        //
        btnColetarPendentes.AutoSize = false;
        btnColetarPendentes.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnColetarPendentes.Font = new Font("Segoe UI", 8.5F);
        btnColetarPendentes.Margin = new Padding(0, 0, 4, 0);
        btnColetarPendentes.Name = "btnColetarPendentes";
        btnColetarPendentes.Size = new Size(142, 32);
        btnColetarPendentes.Text = "Simular Emissão (F10)";
        btnColetarPendentes.ToolTipText = "Simular a emissão por meio da coleta de documentos pendentes; não altera o fluxo de recepção configurado (F10)";
        btnColetarPendentes.Click += btnColetarPendentes_Click;
        //
        // btnConfirmarAck
        //
        btnConfirmarAck.AutoSize = false;
        btnConfirmarAck.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnConfirmarAck.Font = new Font("Segoe UI", 8.5F);
        btnConfirmarAck.Margin = new Padding(0, 0, 4, 0);
        btnConfirmarAck.Name = "btnConfirmarAck";
        btnConfirmarAck.Size = new Size(190, 32);
        btnConfirmarAck.Text = "Confirmar Recebimento (F8)";
        btnConfirmarAck.ToolTipText = "Confirmar o recebimento da sequência coletada na emissão (ACK); não altera o fluxo de retorno configurado (F8)";
        btnConfirmarAck.Click += btnConfirmarAck_Click;
        //
        // btnLimpar
        //
        btnLimpar.AutoSize = false;
        btnLimpar.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnLimpar.Font = new Font("Segoe UI", 8.5F);
        btnLimpar.Margin = new Padding(0, 0, 4, 0);
        btnLimpar.Name = "btnLimpar";
        btnLimpar.Size = new Size(100, 32);
        btnLimpar.Text = "Limpar (F12)";
        btnLimpar.ToolTipText = "Limpar o documento e os resultados do retorno, mantendo a configuração da API (F12)";
        btnLimpar.Click += btnLimpar_Click;
        //
        // btnSalvarXmlAlterado
        //
        btnSalvarXmlAlterado.AutoSize = false;
        btnSalvarXmlAlterado.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnSalvarXmlAlterado.Font = new Font("Segoe UI", 8.5F);
        btnSalvarXmlAlterado.Margin = new Padding(0, 0, 4, 0);
        btnSalvarXmlAlterado.Name = "btnSalvarXmlAlterado";
        btnSalvarXmlAlterado.Size = new Size(155, 32);
        btnSalvarXmlAlterado.Text = "Salvar XML Alterado";
        btnSalvarXmlAlterado.ToolTipText = "Salvar em arquivo o XML que foi alterado neste simulador";
        btnSalvarXmlAlterado.Click += btnSalvarXmlAlterado_Click;
        //
        // btnLimparColeta
        //
        btnLimparColeta.AutoSize = false;
        btnLimparColeta.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnLimparColeta.Font = new Font("Segoe UI", 8.5F);
        btnLimparColeta.Margin = new Padding(0, 0, 4, 0);
        btnLimparColeta.Name = "btnLimparColeta";
        btnLimparColeta.Size = new Size(100, 32);
        btnLimparColeta.Text = "Limpar (F12)";
        btnLimparColeta.ToolTipText = "Limpar os dados e os resultados da coleta, mantendo a configuração da API (F12)";
        btnLimparColeta.Click += btnLimparColeta_Click;
        //
        // tabelaPrincipal
        //
        tabelaPrincipal.ColumnCount = 1;
        tabelaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tabelaPrincipal.Controls.Add(tabProcessos, 0, 0);
        tabelaPrincipal.Dock = DockStyle.Fill;
        tabelaPrincipal.Location = new Point(0, 24);
        tabelaPrincipal.Name = "tabelaPrincipal";
        tabelaPrincipal.Padding = new Padding(12, 8, 12, 8);
        tabelaPrincipal.RowCount = 1;
        tabelaPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaPrincipal.Size = new Size(1184, 734);
        tabelaPrincipal.TabIndex = 1;
        //
        // tabProcessos
        //
        tabProcessos.Controls.Add(tabRetorno);
        tabProcessos.Controls.Add(tabColeta);
        tabProcessos.Dock = DockStyle.Fill;
        tabProcessos.Location = new Point(15, 11);
        tabProcessos.Name = "tabProcessos";
        tabProcessos.SelectedIndex = 0;
        tabProcessos.Size = new Size(1154, 712);
        tabProcessos.TabIndex = 0;
        tabProcessos.SelectedIndexChanged += tabProcessos_SelectedIndexChanged;
        //
        // tabRetorno
        //
        tabRetorno.Controls.Add(tabelaRetorno);
        tabRetorno.Location = new Point(4, 24);
        tabRetorno.Name = "tabRetorno";
        tabRetorno.Padding = new Padding(3);
        tabRetorno.Size = new Size(1146, 684);
        tabRetorno.TabIndex = 0;
        tabRetorno.Text = "Recepção";
        tabRetorno.UseVisualStyleBackColor = true;
        //
        // tabelaRetorno
        //
        tabelaRetorno.ColumnCount = 1;
        tabelaRetorno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tabelaRetorno.Controls.Add(tabelaParametrosRetorno, 0, 0);
        tabelaRetorno.Controls.Add(toolRetorno, 0, 1);
        tabelaRetorno.Controls.Add(splitConteudo, 0, 2);
        tabelaRetorno.Dock = DockStyle.Fill;
        tabelaRetorno.Location = new Point(3, 3);
        tabelaRetorno.Name = "tabelaRetorno";
        tabelaRetorno.RowCount = 3;
        tabelaRetorno.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tabelaRetorno.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        tabelaRetorno.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaRetorno.Size = new Size(1140, 678);
        tabelaRetorno.TabIndex = 0;
        //
        // tabelaParametrosRetorno
        //
        tabelaParametrosRetorno.ColumnCount = 4;
        tabelaParametrosRetorno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
        tabelaParametrosRetorno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
        tabelaParametrosRetorno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
        tabelaParametrosRetorno.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
        tabelaParametrosRetorno.Controls.Add(lblTipoDocumentoRetorno, 0, 0);
        tabelaParametrosRetorno.Controls.Add(cmbTipoDocumentoRetorno, 0, 1);
        tabelaParametrosRetorno.Controls.Add(lblFluxoRetorno, 1, 0);
        tabelaParametrosRetorno.Controls.Add(cmbFluxoRetorno, 1, 1);
        tabelaParametrosRetorno.Controls.Add(lblSequenciaRetorno, 2, 0);
        tabelaParametrosRetorno.Controls.Add(nudSequenciaRetorno, 2, 1);
        tabelaParametrosRetorno.Controls.Add(lblPontoImpressaoRetorno, 3, 0);
        tabelaParametrosRetorno.Controls.Add(txtPontoImpressaoRetorno, 3, 1);
        tabelaParametrosRetorno.Dock = DockStyle.Fill;
        tabelaParametrosRetorno.Location = new Point(3, 3);
        tabelaParametrosRetorno.Name = "tabelaParametrosRetorno";
        tabelaParametrosRetorno.Padding = new Padding(4, 2, 4, 2);
        tabelaParametrosRetorno.RowCount = 2;
        tabelaParametrosRetorno.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaParametrosRetorno.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaParametrosRetorno.Size = new Size(1134, 50);
        tabelaParametrosRetorno.TabIndex = 0;
        //
        // lblTipoDocumentoRetorno
        //
        lblTipoDocumentoRetorno.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblTipoDocumentoRetorno.AutoSize = true;
        lblTipoDocumentoRetorno.Location = new Point(7, 6);
        lblTipoDocumentoRetorno.Name = "lblTipoDocumentoRetorno";
        lblTipoDocumentoRetorno.Size = new Size(113, 15);
        lblTipoDocumentoRetorno.TabIndex = 0;
        lblTipoDocumentoRetorno.Text = "Tipo de Documento";
        //
        // cmbTipoDocumentoRetorno
        //
        cmbTipoDocumentoRetorno.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbTipoDocumentoRetorno.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTipoDocumentoRetorno.FormattingEnabled = true;
        cmbTipoDocumentoRetorno.Items.AddRange(new object[] { "NF-e (XML nfeProc)", "CT-e (XML cteProc ou cteSimpProc)", "NFS-e (JSON)", "MDF-e (XML mdfeProc de retorno)" });
        cmbTipoDocumentoRetorno.Location = new Point(7, 24);
        cmbTipoDocumentoRetorno.Name = "cmbTipoDocumentoRetorno";
        cmbTipoDocumentoRetorno.Size = new Size(376, 23);
        cmbTipoDocumentoRetorno.TabIndex = 1;
        cmbTipoDocumentoRetorno.SelectedIndexChanged += cmbTipoDocumentoRetorno_SelectedIndexChanged;
        //
        // lblFluxoRetorno
        //
        lblFluxoRetorno.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblFluxoRetorno.AutoSize = true;
        lblFluxoRetorno.Location = new Point(389, 6);
        lblFluxoRetorno.Name = "lblFluxoRetorno";
        lblFluxoRetorno.Size = new Size(130, 15);
        lblFluxoRetorno.TabIndex = 2;
        lblFluxoRetorno.Text = "Origem do Documento";
        //
        // cmbFluxoRetorno
        //
        cmbFluxoRetorno.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbFluxoRetorno.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFluxoRetorno.FormattingEnabled = true;
        cmbFluxoRetorno.Items.AddRange(new object[] { "Emitido pelo ERP", "Recebido de Terceiro" });
        cmbFluxoRetorno.Location = new Point(389, 24);
        cmbFluxoRetorno.Name = "cmbFluxoRetorno";
        cmbFluxoRetorno.Size = new Size(241, 23);
        cmbFluxoRetorno.TabIndex = 3;
        cmbFluxoRetorno.SelectedIndexChanged += cmbFluxoRetorno_SelectedIndexChanged;
        //
        // lblSequenciaRetorno
        //
        lblSequenciaRetorno.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblSequenciaRetorno.AutoSize = true;
        lblSequenciaRetorno.Location = new Point(636, 6);
        lblSequenciaRetorno.Name = "lblSequenciaRetorno";
        lblSequenciaRetorno.Size = new Size(84, 15);
        lblSequenciaRetorno.TabIndex = 4;
        lblSequenciaRetorno.Text = "Sequência DFe";
        //
        // nudSequenciaRetorno
        //
        nudSequenciaRetorno.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        nudSequenciaRetorno.Location = new Point(636, 24);
        nudSequenciaRetorno.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
        nudSequenciaRetorno.Name = "nudSequenciaRetorno";
        nudSequenciaRetorno.Size = new Size(196, 23);
        nudSequenciaRetorno.TabIndex = 5;
        nudSequenciaRetorno.ValueChanged += nudSequenciaRetorno_ValueChanged;
        //
        // lblPontoImpressaoRetorno
        //
        lblPontoImpressaoRetorno.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblPontoImpressaoRetorno.AutoSize = true;
        lblPontoImpressaoRetorno.Location = new Point(838, 6);
        lblPontoImpressaoRetorno.Name = "lblPontoImpressaoRetorno";
        lblPontoImpressaoRetorno.Size = new Size(112, 15);
        lblPontoImpressaoRetorno.TabIndex = 6;
        lblPontoImpressaoRetorno.Text = "Ponto de Impressão";
        //
        // txtPontoImpressaoRetorno
        //
        txtPontoImpressaoRetorno.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPontoImpressaoRetorno.Location = new Point(838, 24);
        txtPontoImpressaoRetorno.Name = "txtPontoImpressaoRetorno";
        txtPontoImpressaoRetorno.Size = new Size(289, 23);
        txtPontoImpressaoRetorno.TabIndex = 7;
        txtPontoImpressaoRetorno.TextChanged += txtPontoImpressaoRetorno_TextChanged;
        //
        // toolRetorno
        //
        toolRetorno.AutoSize = false;
        toolRetorno.Dock = DockStyle.Fill;
        toolRetorno.GripStyle = ToolStripGripStyle.Hidden;
        toolRetorno.ImageScalingSize = new Size(20, 20);
        toolRetorno.Items.AddRange(new ToolStripItem[] { btnAbrirArquivo, btnEnviar, btnLimpar, btnSalvarXmlAlterado });
        toolRetorno.Location = new Point(0, 56);
        toolRetorno.Name = "toolRetorno";
        toolRetorno.Padding = new Padding(4);
        toolRetorno.Size = new Size(1140, 42);
        toolRetorno.TabIndex = 1;
        //
        // splitConteudo
        //
        splitConteudo.Dock = DockStyle.Fill;
        splitConteudo.Location = new Point(3, 101);
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
        splitConteudo.Size = new Size(1134, 574);
        splitConteudo.SplitterDistance = 460;
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
        tabDocumento.Size = new Size(1134, 460);
        tabDocumento.TabIndex = 0;
        //
        // tabEdicaoCampos
        //
        tabEdicaoCampos.Controls.Add(splitCampos);
        tabEdicaoCampos.Location = new Point(4, 24);
        tabEdicaoCampos.Name = "tabEdicaoCampos";
        tabEdicaoCampos.Padding = new Padding(3);
        tabEdicaoCampos.Size = new Size(1126, 432);
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
        splitCampos.Size = new Size(1120, 426);
        splitCampos.SplitterDistance = 233;
        splitCampos.TabIndex = 0;
        //
        // arvoreCampos
        //
        arvoreCampos.AllowDrop = true;
        arvoreCampos.Dock = DockStyle.Fill;
        arvoreCampos.HideSelection = false;
        arvoreCampos.Location = new Point(0, 47);
        arvoreCampos.Name = "arvoreCampos";
        arvoreCampos.Size = new Size(1120, 186);
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
        txtPesquisarTags.Size = new Size(1120, 23);
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
        lblArvoreCampos.Size = new Size(1120, 24);
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
        painelEdicaoCampo.Size = new Size(1120, 189);
        painelEdicaoCampo.TabIndex = 0;
        painelEdicaoCampo.DragDrop += txtDocumento_DragDrop;
        painelEdicaoCampo.DragEnter += txtDocumento_DragEnter;
        //
        // grupoAtributos
        //
        grupoAtributos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grupoAtributos.Controls.Add(painelAtributos);
        grupoAtributos.Location = new Point(4, 96);
        grupoAtributos.Name = "grupoAtributos";
        grupoAtributos.Padding = new Padding(8, 4, 8, 6);
        grupoAtributos.Size = new Size(1112, 79);
        grupoAtributos.TabIndex = 4;
        grupoAtributos.TabStop = false;
        grupoAtributos.Text = "Atributos";
        //
        // painelAtributos
        //
        painelAtributos.AutoScroll = true;
        painelAtributos.Dock = DockStyle.Fill;
        painelAtributos.Location = new Point(8, 20);
        painelAtributos.Name = "painelAtributos";
        painelAtributos.Size = new Size(1096, 53);
        painelAtributos.TabIndex = 0;
        //
        // txtValorCampo
        //
        txtValorCampo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtValorCampo.Location = new Point(4, 68);
        txtValorCampo.Name = "txtValorCampo";
        txtValorCampo.Size = new Size(1112, 23);
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
        txtCaminhoCampo.Size = new Size(1112, 23);
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
        tabConteudoDocumento.Size = new Size(1126, 432);
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
        txtDocumento.Size = new Size(1120, 402);
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
        lblDocumento.Size = new Size(1120, 24);
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
        tabResultado.Size = new Size(1134, 110);
        tabResultado.TabIndex = 0;
        tabResultado.SelectedIndexChanged += tabResultado_SelectedIndexChanged;
        //
        // tabJson
        //
        tabJson.Controls.Add(txtJson);
        tabJson.Location = new Point(4, 24);
        tabJson.Name = "tabJson";
        tabJson.Padding = new Padding(3);
        tabJson.Size = new Size(1126, 82);
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
        txtJson.Size = new Size(1120, 76);
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
        tabRequisicao.Size = new Size(1126, 82);
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
        txtRequisicao.Size = new Size(1120, 76);
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
        tabResposta.Size = new Size(1126, 82);
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
        txtResposta.Size = new Size(1120, 76);
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
        tabHistorico.Size = new Size(1126, 82);
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
        txtHistorico.Size = new Size(1120, 76);
        txtHistorico.TabIndex = 0;
        txtHistorico.Text = "";
        txtHistorico.WordWrap = false;
        //
        // tabColeta
        //
        tabColeta.Controls.Add(tabelaColeta);
        tabColeta.Location = new Point(4, 24);
        tabColeta.Name = "tabColeta";
        tabColeta.Padding = new Padding(3);
        tabColeta.Size = new Size(1146, 684);
        tabColeta.TabIndex = 1;
        tabColeta.Text = "Emissão";
        tabColeta.UseVisualStyleBackColor = true;
        //
        // tabelaColeta
        //
        tabelaColeta.ColumnCount = 1;
        tabelaColeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tabelaColeta.Controls.Add(tabelaParametrosColeta, 0, 0);
        tabelaColeta.Controls.Add(toolColeta, 0, 1);
        tabelaColeta.Controls.Add(tabResultadoColeta, 0, 2);
        tabelaColeta.Dock = DockStyle.Fill;
        tabelaColeta.Location = new Point(3, 3);
        tabelaColeta.Name = "tabelaColeta";
        tabelaColeta.RowCount = 3;
        tabelaColeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tabelaColeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        tabelaColeta.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaColeta.Size = new Size(1140, 678);
        tabelaColeta.TabIndex = 0;
        //
        // tabelaParametrosColeta
        //
        tabelaParametrosColeta.ColumnCount = 2;
        tabelaParametrosColeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
        tabelaParametrosColeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
        tabelaParametrosColeta.Controls.Add(lblTipoDocumentoColeta, 0, 0);
        tabelaParametrosColeta.Controls.Add(cmbTipoDocumentoColeta, 0, 1);
        tabelaParametrosColeta.Controls.Add(lblOrientacaoColeta, 1, 0);
        tabelaParametrosColeta.Dock = DockStyle.Fill;
        tabelaParametrosColeta.Location = new Point(3, 3);
        tabelaParametrosColeta.Name = "tabelaParametrosColeta";
        tabelaParametrosColeta.Padding = new Padding(4, 2, 4, 2);
        tabelaParametrosColeta.RowCount = 2;
        tabelaParametrosColeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
        tabelaParametrosColeta.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tabelaParametrosColeta.Size = new Size(1134, 50);
        tabelaParametrosColeta.TabIndex = 0;
        //
        // lblTipoDocumentoColeta
        //
        lblTipoDocumentoColeta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblTipoDocumentoColeta.AutoSize = true;
        lblTipoDocumentoColeta.Location = new Point(7, 6);
        lblTipoDocumentoColeta.Name = "lblTipoDocumentoColeta";
        lblTipoDocumentoColeta.Size = new Size(113, 15);
        lblTipoDocumentoColeta.TabIndex = 0;
        lblTipoDocumentoColeta.Text = "Tipo de Documento";
        //
        // cmbTipoDocumentoColeta
        //
        cmbTipoDocumentoColeta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbTipoDocumentoColeta.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTipoDocumentoColeta.FormattingEnabled = true;
        cmbTipoDocumentoColeta.Items.AddRange(new object[] { "NF-e", "CT-e", "NFS-e", "MDF-e" });
        cmbTipoDocumentoColeta.Location = new Point(7, 24);
        cmbTipoDocumentoColeta.Name = "cmbTipoDocumentoColeta";
        cmbTipoDocumentoColeta.Size = new Size(376, 23);
        cmbTipoDocumentoColeta.TabIndex = 1;
        cmbTipoDocumentoColeta.SelectedIndexChanged += cmbTipoDocumentoColeta_SelectedIndexChanged;
        //
        // lblOrientacaoColeta
        //
        lblOrientacaoColeta.AutoEllipsis = true;
        lblOrientacaoColeta.Dock = DockStyle.Fill;
        lblOrientacaoColeta.ForeColor = Color.DimGray;
        lblOrientacaoColeta.Location = new Point(389, 2);
        lblOrientacaoColeta.Name = "lblOrientacaoColeta";
        tabelaParametrosColeta.SetRowSpan(lblOrientacaoColeta, 2);
        lblOrientacaoColeta.Size = new Size(738, 46);
        lblOrientacaoColeta.TabIndex = 2;
        lblOrientacaoColeta.Text = "Coleta/confirmação de recebimento: não usa XML nem altera o retorno. A sequência é informada no diálogo de confirmação.";
        lblOrientacaoColeta.TextAlign = ContentAlignment.MiddleLeft;
        lblOrientacaoColeta.Click += lblOrientacaoColeta_Click;
        //
        // toolColeta
        //
        toolColeta.AutoSize = false;
        toolColeta.Dock = DockStyle.Fill;
        toolColeta.GripStyle = ToolStripGripStyle.Hidden;
        toolColeta.ImageScalingSize = new Size(20, 20);
        toolColeta.Items.AddRange(new ToolStripItem[] { btnColetarPendentes, btnConfirmarAck, btnLimparColeta });
        toolColeta.Location = new Point(0, 56);
        toolColeta.Name = "toolColeta";
        toolColeta.Padding = new Padding(4);
        toolColeta.Size = new Size(1140, 42);
        toolColeta.TabIndex = 1;
        //
        // tabResultadoColeta
        //
        tabResultadoColeta.Controls.Add(tabRequisicaoColeta);
        tabResultadoColeta.Controls.Add(tabRespostaColeta);
        tabResultadoColeta.Controls.Add(tabHistoricoColeta);
        tabResultadoColeta.Dock = DockStyle.Fill;
        tabResultadoColeta.Location = new Point(3, 101);
        tabResultadoColeta.Name = "tabResultadoColeta";
        tabResultadoColeta.SelectedIndex = 0;
        tabResultadoColeta.Size = new Size(1134, 574);
        tabResultadoColeta.TabIndex = 0;
        tabResultadoColeta.SelectedIndexChanged += tabResultadoColeta_SelectedIndexChanged;
        //
        // tabRequisicaoColeta
        //
        tabRequisicaoColeta.Controls.Add(txtRequisicaoColeta);
        tabRequisicaoColeta.Location = new Point(4, 24);
        tabRequisicaoColeta.Name = "tabRequisicaoColeta";
        tabRequisicaoColeta.Padding = new Padding(3);
        tabRequisicaoColeta.Size = new Size(1126, 546);
        tabRequisicaoColeta.TabIndex = 0;
        tabRequisicaoColeta.Text = "Requisição";
        tabRequisicaoColeta.UseVisualStyleBackColor = true;
        //
        // txtRequisicaoColeta
        //
        txtRequisicaoColeta.BackColor = Color.White;
        txtRequisicaoColeta.Dock = DockStyle.Fill;
        txtRequisicaoColeta.Font = new Font("Consolas", 9F);
        txtRequisicaoColeta.Location = new Point(3, 3);
        txtRequisicaoColeta.Name = "txtRequisicaoColeta";
        txtRequisicaoColeta.ReadOnly = true;
        txtRequisicaoColeta.Size = new Size(1120, 540);
        txtRequisicaoColeta.TabIndex = 0;
        txtRequisicaoColeta.Text = "";
        txtRequisicaoColeta.WordWrap = false;
        //
        // tabRespostaColeta
        //
        tabRespostaColeta.Controls.Add(txtRespostaColeta);
        tabRespostaColeta.Location = new Point(4, 24);
        tabRespostaColeta.Name = "tabRespostaColeta";
        tabRespostaColeta.Padding = new Padding(3);
        tabRespostaColeta.Size = new Size(1126, 546);
        tabRespostaColeta.TabIndex = 1;
        tabRespostaColeta.Text = "Resposta da API";
        tabRespostaColeta.UseVisualStyleBackColor = true;
        //
        // txtRespostaColeta
        //
        txtRespostaColeta.BackColor = Color.White;
        txtRespostaColeta.Dock = DockStyle.Fill;
        txtRespostaColeta.Font = new Font("Consolas", 9F);
        txtRespostaColeta.Location = new Point(3, 3);
        txtRespostaColeta.Name = "txtRespostaColeta";
        txtRespostaColeta.ReadOnly = true;
        txtRespostaColeta.Size = new Size(1120, 540);
        txtRespostaColeta.TabIndex = 0;
        txtRespostaColeta.Text = "";
        txtRespostaColeta.WordWrap = false;
        //
        // tabHistoricoColeta
        //
        tabHistoricoColeta.Controls.Add(txtHistoricoColeta);
        tabHistoricoColeta.Location = new Point(4, 24);
        tabHistoricoColeta.Name = "tabHistoricoColeta";
        tabHistoricoColeta.Padding = new Padding(3);
        tabHistoricoColeta.Size = new Size(1126, 546);
        tabHistoricoColeta.TabIndex = 2;
        tabHistoricoColeta.Text = "Histórico da Coleta";
        tabHistoricoColeta.UseVisualStyleBackColor = true;
        //
        // txtHistoricoColeta
        //
        txtHistoricoColeta.BackColor = Color.White;
        txtHistoricoColeta.Dock = DockStyle.Fill;
        txtHistoricoColeta.Font = new Font("Consolas", 9F);
        txtHistoricoColeta.Location = new Point(3, 3);
        txtHistoricoColeta.Name = "txtHistoricoColeta";
        txtHistoricoColeta.ReadOnly = true;
        txtHistoricoColeta.Size = new Size(1120, 540);
        txtHistoricoColeta.TabIndex = 0;
        txtHistoricoColeta.Text = "";
        txtHistoricoColeta.WordWrap = false;
        //
        // statusStrip
        //
        statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus, lblIndicadorStatus });
        statusStrip.Location = new Point(0, 758);
        statusStrip.Name = "statusStrip";
        statusStrip.Padding = new Padding(12, 0, 1, 0);
        statusStrip.Size = new Size(1184, 26);
        statusStrip.SizingGrip = false;
        statusStrip.TabIndex = 2;
        //
        // lblStatus
        //
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(1151, 21);
        lblStatus.Spring = true;
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblIndicadorStatus
        //
        lblIndicadorStatus.Alignment = ToolStripItemAlignment.Right;
        lblIndicadorStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblIndicadorStatus.ForeColor = Color.DimGray;
        lblIndicadorStatus.Name = "lblIndicadorStatus";
        lblIndicadorStatus.Size = new Size(20, 21);
        lblIndicadorStatus.Text = "●";
        lblIndicadorStatus.ToolTipText = "Nenhuma requisição concluída nesta sessão.";
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
        Controls.Add(menuPrincipal);
        KeyPreview = true;
        MainMenuStrip = menuPrincipal;
        MinimumSize = new Size(1024, 700);
        Name = "TelaPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Simulador DFe — Emissão e Recepção";
        KeyDown += TelaPrincipal_KeyDown;
        menuPrincipal.ResumeLayout(false);
        menuPrincipal.PerformLayout();
        tabelaPrincipal.ResumeLayout(false);
        tabProcessos.ResumeLayout(false);
        tabRetorno.ResumeLayout(false);
        tabelaRetorno.ResumeLayout(false);
        tabelaParametrosRetorno.ResumeLayout(false);
        tabelaParametrosRetorno.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSequenciaRetorno).EndInit();
        toolRetorno.ResumeLayout(false);
        toolRetorno.PerformLayout();
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
        grupoAtributos.ResumeLayout(false);
        tabConteudoDocumento.ResumeLayout(false);
        tabResultado.ResumeLayout(false);
        tabJson.ResumeLayout(false);
        tabRequisicao.ResumeLayout(false);
        tabResposta.ResumeLayout(false);
        tabHistorico.ResumeLayout(false);
        tabColeta.ResumeLayout(false);
        tabelaColeta.ResumeLayout(false);
        tabelaParametrosColeta.ResumeLayout(false);
        tabelaParametrosColeta.PerformLayout();
        toolColeta.ResumeLayout(false);
        toolColeta.PerformLayout();
        tabResultadoColeta.ResumeLayout(false);
        tabRequisicaoColeta.ResumeLayout(false);
        tabRespostaColeta.ResumeLayout(false);
        tabHistoricoColeta.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

}
