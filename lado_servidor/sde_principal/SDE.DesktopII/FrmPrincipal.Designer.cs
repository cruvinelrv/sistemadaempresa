namespace SDE.Desktop
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.IntrovertIMApp = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mostrarSDEDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sistemaDaEmpresaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nFeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multisoftLojaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiTEFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenteDeCaixaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fRENTEDECAIXAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eNCERRAMENTODECAIXAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cONSULTADEPAGAMENTOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multisoftSETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tEFTYNAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapaResumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sINTEGRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackuptoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cONFIGURACOESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTipMenu = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.IntrovertIMApp)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // IntrovertIMApp
            // 
            this.IntrovertIMApp.Enabled = true;
            this.IntrovertIMApp.Location = new System.Drawing.Point(386, 166);
            this.IntrovertIMApp.Name = "IntrovertIMApp";
            this.IntrovertIMApp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("IntrovertIMApp.OcxState")));
            this.IntrovertIMApp.Size = new System.Drawing.Size(600, 300);
            this.IntrovertIMApp.TabIndex = 4;
            this.IntrovertIMApp.Visible = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "SDE Desktop Ativo";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "SDE Desktop";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostrarSDEDesktopToolStripMenuItem,
            this.toolStripSeparator1});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(187, 32);
            // 
            // mostrarSDEDesktopToolStripMenuItem
            // 
            this.mostrarSDEDesktopToolStripMenuItem.Name = "mostrarSDEDesktopToolStripMenuItem";
            this.mostrarSDEDesktopToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.mostrarSDEDesktopToolStripMenuItem.Text = "Mostrar SDE Desktop";
            this.mostrarSDEDesktopToolStripMenuItem.Click += new System.EventHandler(this.mostrarSDEDesktopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 104);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1024, 630);
            this.webBrowser1.TabIndex = 5;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(1020, 700);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sistemaDaEmpresaToolStripMenuItem,
            this.manualToolStripMenuItem,
            this.nFeToolStripMenuItem,
            this.multisoftLojaToolStripMenuItem,
            this.multiTEFToolStripMenuItem,
            this.frenteDeCaixaToolStripMenuItem,
            this.multisoftSETToolStripMenuItem,
            this.tEFTYNAToolStripMenuItem,
            this.mapaResumpToolStripMenuItem,
            this.sINTEGRAToolStripMenuItem,
            this.BackuptoolStripMenuItem,
            this.cONFIGURACOESToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1026, 101);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sistemaDaEmpresaToolStripMenuItem
            // 
            this.sistemaDaEmpresaToolStripMenuItem.AutoSize = false;
            this.sistemaDaEmpresaToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sistemaDaEmpresaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sistemaDaEmpresaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sistemaDaEmpresaToolStripMenuItem.Image")));
            this.sistemaDaEmpresaToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sistemaDaEmpresaToolStripMenuItem.Name = "sistemaDaEmpresaToolStripMenuItem";
            this.sistemaDaEmpresaToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.sistemaDaEmpresaToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sistemaDaEmpresaToolStripMenuItem.MouseEnter += new System.EventHandler(this.sistemaDaEmpresaToolStripMenuItem_MouseEnter);
            this.sistemaDaEmpresaToolStripMenuItem.Click += new System.EventHandler(this.sistemaDaEmpresaToolStripMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.AutoSize = false;
            this.manualToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.manualToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manualToolStripMenuItem.Image")));
            this.manualToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.manualToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.manualToolStripMenuItem.MouseEnter += new System.EventHandler(this.manualToolStripMenuItem_MouseEnter);
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // nFeToolStripMenuItem
            // 
            this.nFeToolStripMenuItem.AutoSize = false;
            this.nFeToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nFeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nFeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nFeToolStripMenuItem.Image")));
            this.nFeToolStripMenuItem.Name = "nFeToolStripMenuItem";
            this.nFeToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.nFeToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.nFeToolStripMenuItem.MouseEnter += new System.EventHandler(this.nFeToolStripMenuItem_MouseEnter);
            this.nFeToolStripMenuItem.Click += new System.EventHandler(this.nFeToolStripMenuItem_Click);
            // 
            // multisoftLojaToolStripMenuItem
            // 
            this.multisoftLojaToolStripMenuItem.AutoSize = false;
            this.multisoftLojaToolStripMenuItem.Enabled = false;
            this.multisoftLojaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("multisoftLojaToolStripMenuItem.Image")));
            this.multisoftLojaToolStripMenuItem.Name = "multisoftLojaToolStripMenuItem";
            this.multisoftLojaToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.multisoftLojaToolStripMenuItem.MouseEnter += new System.EventHandler(this.multisoftLojaToolStripMenuItem_MouseEnter);
            this.multisoftLojaToolStripMenuItem.Click += new System.EventHandler(this.multisoftLojaToolStripMenuItem_Click);
            // 
            // multiTEFToolStripMenuItem
            // 
            this.multiTEFToolStripMenuItem.AutoSize = false;
            this.multiTEFToolStripMenuItem.Enabled = false;
            this.multiTEFToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("multiTEFToolStripMenuItem.Image")));
            this.multiTEFToolStripMenuItem.Name = "multiTEFToolStripMenuItem";
            this.multiTEFToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.multiTEFToolStripMenuItem.MouseEnter += new System.EventHandler(this.multisoftSETToolStripMenuItem_MouseEnter);
            this.multiTEFToolStripMenuItem.Click += new System.EventHandler(this.multiTEFToolStripMenuItem_Click);
            // 
            // frenteDeCaixaToolStripMenuItem
            // 
            this.frenteDeCaixaToolStripMenuItem.AutoSize = false;
            this.frenteDeCaixaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fRENTEDECAIXAToolStripMenuItem1,
            this.eNCERRAMENTODECAIXAToolStripMenuItem,
            this.cONSULTADEPAGAMENTOSToolStripMenuItem});
            this.frenteDeCaixaToolStripMenuItem.Enabled = false;
            this.frenteDeCaixaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("frenteDeCaixaToolStripMenuItem.Image")));
            this.frenteDeCaixaToolStripMenuItem.Name = "frenteDeCaixaToolStripMenuItem";
            this.frenteDeCaixaToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.frenteDeCaixaToolStripMenuItem.MouseEnter += new System.EventHandler(this.frenteDeCaixaToolStripMenuItem_MouseEnter);
            // 
            // fRENTEDECAIXAToolStripMenuItem1
            // 
            this.fRENTEDECAIXAToolStripMenuItem1.AutoSize = false;
            this.fRENTEDECAIXAToolStripMenuItem1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fRENTEDECAIXAToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("fRENTEDECAIXAToolStripMenuItem1.Image")));
            this.fRENTEDECAIXAToolStripMenuItem1.Name = "fRENTEDECAIXAToolStripMenuItem1";
            this.fRENTEDECAIXAToolStripMenuItem1.Size = new System.Drawing.Size(400, 54);
            this.fRENTEDECAIXAToolStripMenuItem1.Text = "FRENTE DE CAIXA";
            this.fRENTEDECAIXAToolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.fRENTEDECAIXAToolStripMenuItem1.Click += new System.EventHandler(this.fRENTEDECAIXAToolStripMenuItem1_Click);
            // 
            // eNCERRAMENTODECAIXAToolStripMenuItem
            // 
            this.eNCERRAMENTODECAIXAToolStripMenuItem.AutoSize = false;
            this.eNCERRAMENTODECAIXAToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eNCERRAMENTODECAIXAToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eNCERRAMENTODECAIXAToolStripMenuItem.Image")));
            this.eNCERRAMENTODECAIXAToolStripMenuItem.Name = "eNCERRAMENTODECAIXAToolStripMenuItem";
            this.eNCERRAMENTODECAIXAToolStripMenuItem.Size = new System.Drawing.Size(400, 54);
            this.eNCERRAMENTODECAIXAToolStripMenuItem.Text = "ENCERRAMENTO DE CAIXA";
            this.eNCERRAMENTODECAIXAToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.eNCERRAMENTODECAIXAToolStripMenuItem.Click += new System.EventHandler(this.eNCERRAMENTODECAIXAToolStripMenuItem_Click);
            // 
            // cONSULTADEPAGAMENTOSToolStripMenuItem
            // 
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.AutoSize = false;
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cONSULTADEPAGAMENTOSToolStripMenuItem.Image")));
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.Name = "cONSULTADEPAGAMENTOSToolStripMenuItem";
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.Size = new System.Drawing.Size(400, 54);
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.Text = "CONSULTA DE PAGAMENTOS";
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cONSULTADEPAGAMENTOSToolStripMenuItem.Click += new System.EventHandler(this.cONSULTADEPAGAMENTOSToolStripMenuItem_Click);
            // 
            // multisoftSETToolStripMenuItem
            // 
            this.multisoftSETToolStripMenuItem.AutoSize = false;
            this.multisoftSETToolStripMenuItem.Enabled = false;
            this.multisoftSETToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("multisoftSETToolStripMenuItem.Image")));
            this.multisoftSETToolStripMenuItem.Name = "multisoftSETToolStripMenuItem";
            this.multisoftSETToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.multisoftSETToolStripMenuItem.MouseEnter += new System.EventHandler(this.multisoftSETToolStripMenuItem_MouseEnter);
            this.multisoftSETToolStripMenuItem.Click += new System.EventHandler(this.multisoftSETToolStripMenuItem_Click);
            // 
            // tEFTYNAToolStripMenuItem
            // 
            this.tEFTYNAToolStripMenuItem.AutoSize = false;
            this.tEFTYNAToolStripMenuItem.Enabled = false;
            this.tEFTYNAToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tEFTYNAToolStripMenuItem.Image")));
            this.tEFTYNAToolStripMenuItem.Name = "tEFTYNAToolStripMenuItem";
            this.tEFTYNAToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.tEFTYNAToolStripMenuItem.MouseEnter += new System.EventHandler(this.tEFTYNAToolStripMenuItem_MouseEnter);
            this.tEFTYNAToolStripMenuItem.Click += new System.EventHandler(this.tEFTYNAToolStripMenuItem_Click);
            // 
            // mapaResumpToolStripMenuItem
            // 
            this.mapaResumpToolStripMenuItem.AutoSize = false;
            this.mapaResumpToolStripMenuItem.Enabled = false;
            this.mapaResumpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mapaResumpToolStripMenuItem.Image")));
            this.mapaResumpToolStripMenuItem.Name = "mapaResumpToolStripMenuItem";
            this.mapaResumpToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.mapaResumpToolStripMenuItem.MouseEnter += new System.EventHandler(this.mapaResumpToolStripMenuItem_MouseEnter);
            this.mapaResumpToolStripMenuItem.Click += new System.EventHandler(this.mapaResumpToolStripMenuItem_Click);
            // 
            // sINTEGRAToolStripMenuItem
            // 
            this.sINTEGRAToolStripMenuItem.AutoSize = false;
            this.sINTEGRAToolStripMenuItem.Enabled = false;
            this.sINTEGRAToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sINTEGRAToolStripMenuItem.Image")));
            this.sINTEGRAToolStripMenuItem.Name = "sINTEGRAToolStripMenuItem";
            this.sINTEGRAToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.sINTEGRAToolStripMenuItem.MouseEnter += new System.EventHandler(this.sINTEGRAToolStripMenuItem_MouseEnter);
            this.sINTEGRAToolStripMenuItem.Click += new System.EventHandler(this.sINTEGRAToolStripMenuItem_Click);
            // 
            // BackuptoolStripMenuItem
            // 
            this.BackuptoolStripMenuItem.AutoSize = false;
            this.BackuptoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("BackuptoolStripMenuItem.Image")));
            this.BackuptoolStripMenuItem.Name = "BackuptoolStripMenuItem";
            this.BackuptoolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.BackuptoolStripMenuItem.MouseEnter += new System.EventHandler(this.xToolStripMenuItem_MouseEnter);
            this.BackuptoolStripMenuItem.Click += new System.EventHandler(this.BackuptoolStripMenuItem_Click);
            // 
            // cONFIGURACOESToolStripMenuItem
            // 
            this.cONFIGURACOESToolStripMenuItem.AutoSize = false;
            this.cONFIGURACOESToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cONFIGURACOESToolStripMenuItem.Image")));
            this.cONFIGURACOESToolStripMenuItem.Name = "cONFIGURACOESToolStripMenuItem";
            this.cONFIGURACOESToolStripMenuItem.Size = new System.Drawing.Size(85, 100);
            this.cONFIGURACOESToolStripMenuItem.MouseEnter += new System.EventHandler(this.cONFIGURACOESToolStripMenuItem_MouseEnter);
            this.cONFIGURACOESToolStripMenuItem.Click += new System.EventHandler(this.cONFIGURACOESToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmPrincipal
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 734);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.IntrovertIMApp);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1034, 768);
            this.MinimumSize = new System.Drawing.Size(1034, 768);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDE DESKTOP";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Click += new System.EventHandler(this.FrmPrincipal_Click);
            this.Resize += new System.EventHandler(this.FrmPrincipal_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.IntrovertIMApp)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash IntrovertIMApp;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mostrarSDEDesktopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sistemaDaEmpresaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nFeToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem multisoftLojaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiTEFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenteDeCaixaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multisoftSETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tEFTYNAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapaResumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sINTEGRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackuptoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cONFIGURACOESToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTipMenu;
        private System.Windows.Forms.ToolStripMenuItem fRENTEDECAIXAToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eNCERRAMENTODECAIXAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cONSULTADEPAGAMENTOSToolStripMenuItem;
    }
}

