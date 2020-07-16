namespace ZChangerMMO.Views.Forms
{
    partial class ManageProxy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageProxy));
            this.xtraTabProxyList = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageProxyList = new DevExpress.XtraTab.XtraTabPage();
            this.proxyListTab_ProxyListUserControl = new ZChangerMMO.Views.Controls.ProxyList();
            this.xtraTabPageAddProxy = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.addASingleProxyTab_ProxyListUserControl = new ZChangerMMO.Views.Controls.ProxyList();
            this.addASignleProxy_AddProxyUserControl = new ZChangerMMO.Views.Controls.AddProxy();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPageAddBulk = new DevExpress.XtraTab.XtraTabPage();
            this.addBulkProxiesTab_ProxyListUserControl = new ZChangerMMO.Views.Controls.ProxyList();
            this.addBulkProxies1 = new ZChangerMMO.Views.Controls.AddBulkProxies();
            this.xtraTabPageBypassProxies = new DevExpress.XtraTab.XtraTabPage();
            this.byPassProxy_ByPassProxiesUserControl = new ZChangerMMO.Views.Controls.BypassProxies();
            this.xtraTabPageTestProxies = new DevExpress.XtraTab.XtraTabPage();
            this.testProxiesTab_ProxyListUserControl = new ZChangerMMO.Views.Controls.ProxyList();
            this.testProxyTab_TestProxyUserControl = new ZChangerMMO.Views.Controls.TestProxies();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.testProxyWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabProxyList)).BeginInit();
            this.xtraTabProxyList.SuspendLayout();
            this.xtraTabPageProxyList.SuspendLayout();
            this.xtraTabPageAddProxy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.xtraTabPageAddBulk.SuspendLayout();
            this.xtraTabPageBypassProxies.SuspendLayout();
            this.xtraTabPageTestProxies.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabProxyList
            // 
            this.xtraTabProxyList.Location = new System.Drawing.Point(0, 0);
            this.xtraTabProxyList.Name = "xtraTabProxyList";
            this.xtraTabProxyList.SelectedTabPage = this.xtraTabPageProxyList;
            this.xtraTabProxyList.Size = new System.Drawing.Size(1096, 564);
            this.xtraTabProxyList.TabIndex = 0;
            this.xtraTabProxyList.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageProxyList,
            this.xtraTabPageAddProxy,
            this.xtraTabPageAddBulk,
            this.xtraTabPageBypassProxies,
            this.xtraTabPageTestProxies});
            // 
            // xtraTabPageProxyList
            // 
            this.xtraTabPageProxyList.Controls.Add(this.proxyListTab_ProxyListUserControl);
            this.xtraTabPageProxyList.Name = "xtraTabPageProxyList";
            this.xtraTabPageProxyList.Size = new System.Drawing.Size(1094, 539);
            this.xtraTabPageProxyList.Text = "Proxy List";
            // 
            // proxyListTab_ProxyListUserControl
            // 
            this.proxyListTab_ProxyListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proxyListTab_ProxyListUserControl.Location = new System.Drawing.Point(0, 0);
            this.proxyListTab_ProxyListUserControl.Name = "proxyListTab_ProxyListUserControl";
            this.proxyListTab_ProxyListUserControl.Size = new System.Drawing.Size(1094, 539);
            this.proxyListTab_ProxyListUserControl.TabIndex = 0;
            this.proxyListTab_ProxyListUserControl.Load += new System.EventHandler(this.proxyListTab_ProxyListUserControl_Load);
            // 
            // xtraTabPageAddProxy
            // 
            this.xtraTabPageAddProxy.Controls.Add(this.layoutControl1);
            this.xtraTabPageAddProxy.Name = "xtraTabPageAddProxy";
            this.xtraTabPageAddProxy.Size = new System.Drawing.Size(1094, 539);
            this.xtraTabPageAddProxy.Text = "Add a Single Proxy";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraScrollableControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1094, 539);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.addASingleProxyTab_ProxyListUserControl);
            this.xtraScrollableControl1.Controls.Add(this.addASignleProxy_AddProxyUserControl);
            this.xtraScrollableControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(1090, 535);
            this.xtraScrollableControl1.TabIndex = 4;
            // 
            // addASingleProxyTab_ProxyListUserControl
            // 
            this.addASingleProxyTab_ProxyListUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.addASingleProxyTab_ProxyListUserControl.Location = new System.Drawing.Point(0, 384);
            this.addASingleProxyTab_ProxyListUserControl.Name = "addASingleProxyTab_ProxyListUserControl";
            this.addASingleProxyTab_ProxyListUserControl.Size = new System.Drawing.Size(1073, 512);
            this.addASingleProxyTab_ProxyListUserControl.TabIndex = 1;
            // 
            // addASignleProxy_AddProxyUserControl
            // 
            this.addASignleProxy_AddProxyUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.addASignleProxy_AddProxyUserControl.Location = new System.Drawing.Point(0, 0);
            this.addASignleProxy_AddProxyUserControl.Name = "addASignleProxy_AddProxyUserControl";
            this.addASignleProxy_AddProxyUserControl.Size = new System.Drawing.Size(1073, 384);
            this.addASignleProxy_AddProxyUserControl.TabIndex = 0;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1094, 539);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraScrollableControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1094, 539);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // xtraTabPageAddBulk
            // 
            this.xtraTabPageAddBulk.Controls.Add(this.addBulkProxiesTab_ProxyListUserControl);
            this.xtraTabPageAddBulk.Controls.Add(this.addBulkProxies1);
            this.xtraTabPageAddBulk.Name = "xtraTabPageAddBulk";
            this.xtraTabPageAddBulk.Size = new System.Drawing.Size(1094, 539);
            this.xtraTabPageAddBulk.Text = "Add Bulk Proxies";
            // 
            // addBulkProxiesTab_ProxyListUserControl
            // 
            this.addBulkProxiesTab_ProxyListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addBulkProxiesTab_ProxyListUserControl.Location = new System.Drawing.Point(0, 82);
            this.addBulkProxiesTab_ProxyListUserControl.Name = "addBulkProxiesTab_ProxyListUserControl";
            this.addBulkProxiesTab_ProxyListUserControl.Size = new System.Drawing.Size(1094, 457);
            this.addBulkProxiesTab_ProxyListUserControl.TabIndex = 1;
            // 
            // addBulkProxies1
            // 
            this.addBulkProxies1.Dock = System.Windows.Forms.DockStyle.Top;
            this.addBulkProxies1.Location = new System.Drawing.Point(0, 0);
            this.addBulkProxies1.Name = "addBulkProxies1";
            this.addBulkProxies1.Size = new System.Drawing.Size(1094, 82);
            this.addBulkProxies1.TabIndex = 0;
            // 
            // xtraTabPageBypassProxies
            // 
            this.xtraTabPageBypassProxies.Controls.Add(this.byPassProxy_ByPassProxiesUserControl);
            this.xtraTabPageBypassProxies.Name = "xtraTabPageBypassProxies";
            this.xtraTabPageBypassProxies.Size = new System.Drawing.Size(1094, 539);
            this.xtraTabPageBypassProxies.Text = "Bypass Proxies";
            // 
            // byPassProxy_ByPassProxiesUserControl
            // 
            this.byPassProxy_ByPassProxiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.byPassProxy_ByPassProxiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.byPassProxy_ByPassProxiesUserControl.Name = "byPassProxy_ByPassProxiesUserControl";
            this.byPassProxy_ByPassProxiesUserControl.Size = new System.Drawing.Size(1094, 539);
            this.byPassProxy_ByPassProxiesUserControl.TabIndex = 0;
            // 
            // xtraTabPageTestProxies
            // 
            this.xtraTabPageTestProxies.Controls.Add(this.testProxiesTab_ProxyListUserControl);
            this.xtraTabPageTestProxies.Controls.Add(this.testProxyTab_TestProxyUserControl);
            this.xtraTabPageTestProxies.Name = "xtraTabPageTestProxies";
            this.xtraTabPageTestProxies.Size = new System.Drawing.Size(1094, 539);
            this.xtraTabPageTestProxies.Text = "Test Proxies";
            // 
            // testProxiesTab_ProxyListUserControl
            // 
            this.testProxiesTab_ProxyListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testProxiesTab_ProxyListUserControl.Location = new System.Drawing.Point(0, 93);
            this.testProxiesTab_ProxyListUserControl.Name = "testProxiesTab_ProxyListUserControl";
            this.testProxiesTab_ProxyListUserControl.Size = new System.Drawing.Size(1094, 446);
            this.testProxiesTab_ProxyListUserControl.TabIndex = 1;
            // 
            // testProxyTab_TestProxyUserControl
            // 
            this.testProxyTab_TestProxyUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.testProxyTab_TestProxyUserControl.Location = new System.Drawing.Point(0, 0);
            this.testProxyTab_TestProxyUserControl.Name = "testProxyTab_TestProxyUserControl";
            this.testProxyTab_TestProxyUserControl.Size = new System.Drawing.Size(1094, 93);
            this.testProxyTab_TestProxyUserControl.Status = ZChangerMMO.Views.Controls.TestProxyStatus.Normal;
            this.testProxyTab_TestProxyUserControl.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_Cancel);
            this.flowLayoutPanel1.Controls.Add(this.btn_Save);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 582);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1096, 46);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(1003, 13);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Save.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseForeColor = true;
            this.btn_Save.AppearanceHovered.BackColor = System.Drawing.Color.White;
            this.btn_Save.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Save.Location = new System.Drawing.Point(922, 13);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "Save";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // testProxyWorker
            // 
            this.testProxyWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.testProxyWorker_DoWork);
            this.testProxyWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.testProxyWorker_ProgressChanged);
            this.testProxyWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.testProxyWorker_RunWorkerCompleted);
            // 
            // ManageProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 628);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.xtraTabProxyList);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("ManageProxy.IconOptions.Image")));
            this.Name = "ManageProxy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Proxy";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabProxyList)).EndInit();
            this.xtraTabProxyList.ResumeLayout(false);
            this.xtraTabPageProxyList.ResumeLayout(false);
            this.xtraTabPageAddProxy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.xtraTabPageAddBulk.ResumeLayout(false);
            this.xtraTabPageBypassProxies.ResumeLayout(false);
            this.xtraTabPageTestProxies.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabProxyList;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageProxyList;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAddProxy;
        private Controls.ProxyList proxyListTab_ProxyListUserControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAddBulk;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageBypassProxies;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageTestProxies;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private Controls.AddProxy addASignleProxy_AddProxyUserControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Controls.ProxyList addASingleProxyTab_ProxyListUserControl;
        private Controls.ProxyList addBulkProxiesTab_ProxyListUserControl;
        private Controls.AddBulkProxies addBulkProxies1;
        private Controls.BypassProxies byPassProxy_ByPassProxiesUserControl;
        private Controls.TestProxies testProxyTab_TestProxyUserControl;
        private Controls.ProxyList testProxiesTab_ProxyListUserControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private System.ComponentModel.BackgroundWorker testProxyWorker;
    }
}