namespace ZChangerMMO.Views.Email
{
    partial class EmailListView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailListView));
            this.deviceGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.emailGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmailName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmailAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbtnDeleteProfile = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnEditProfile = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Unregister = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Register = new DevExpress.XtraBars.BarButtonItem();
            this.btn_FocusBrowser = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRandom = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonTongleSelectConfig = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonProxy = new DevExpress.XtraBars.BarButtonItem();
            this.barToggleSwitch_ProxyEnable = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btn_RandomRun = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgProfile = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgManaProfile = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgBrowserAction = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgNativeHost = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbgView = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.toastNotificationsManager1 = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // deviceGridView
            // 
            this.deviceGridView.GridControl = this.gridControl1;
            this.deviceGridView.Name = "deviceGridView";
            // 
            // gridControl1
            // 
            gridLevelNode1.LevelTemplate = this.deviceGridView;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.emailGridView;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(937, 431);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.emailGridView,
            this.deviceGridView});
            // 
            // emailGridView
            // 
            this.emailGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.EmailName,
            this.EmailAccount});
            this.emailGridView.GridControl = this.gridControl1;
            this.emailGridView.Name = "emailGridView";
            // 
            // ID
            // 
            this.ID.Caption = "Email ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            // 
            // EmailName
            // 
            this.EmailName.Caption = "Email Name";
            this.EmailName.FieldName = "Name";
            this.EmailName.Name = "EmailName";
            this.EmailName.OptionsColumn.AllowEdit = false;
            this.EmailName.Visible = true;
            this.EmailName.VisibleIndex = 1;
            // 
            // EmailAccount
            // 
            this.EmailAccount.Caption = "Email Account";
            this.EmailAccount.FieldName = "EmailAccount";
            this.EmailAccount.Name = "EmailAccount";
            this.EmailAccount.OptionsColumn.AllowEdit = false;
            this.EmailAccount.Visible = true;
            this.EmailAccount.VisibleIndex = 2;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.BindingExpressions.AddRange(new DevExpress.Utils.MVVM.BindingExpression[] {
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(ZChangerMMO.ViewModels.EmailListViewModel), "Create", this.btnNew)});
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(ZChangerMMO.ViewModels.EmailListViewModel);
            // 
            // btnNew
            // 
            this.btnNew.Caption = "barButtonItem2";
            this.btnNew.Id = 33;
            this.btnNew.Name = "btnNew";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.bbtnDeleteProfile,
            this.bbtnEditProfile,
            this.btn_Unregister,
            this.btn_Register,
            this.btn_FocusBrowser,
            this.barButtonItemRandom,
            this.barButtonItemSave,
            this.barButtonTongleSelectConfig,
            this.barButtonItem8,
            this.barButtonProxy,
            this.barToggleSwitch_ProxyEnable,
            this.barButtonItem1,
            this.btn_RandomRun,
            this.btnNew});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 34;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
            this.ribbonControl1.Size = new System.Drawing.Size(961, 150);
            // 
            // bbtnDeleteProfile
            // 
            this.bbtnDeleteProfile.Caption = "Delete";
            this.bbtnDeleteProfile.Id = 2;
            this.bbtnDeleteProfile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbtnDeleteProfile.ImageOptions.Image")));
            this.bbtnDeleteProfile.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbtnDeleteProfile.ImageOptions.LargeImage")));
            this.bbtnDeleteProfile.Name = "bbtnDeleteProfile";
            // 
            // bbtnEditProfile
            // 
            this.bbtnEditProfile.Caption = "Edit";
            this.bbtnEditProfile.Id = 3;
            this.bbtnEditProfile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbtnEditProfile.ImageOptions.Image")));
            this.bbtnEditProfile.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbtnEditProfile.ImageOptions.LargeImage")));
            this.bbtnEditProfile.Name = "bbtnEditProfile";
            this.bbtnEditProfile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btn_Unregister
            // 
            this.btn_Unregister.Caption = "Unregister";
            this.btn_Unregister.Id = 23;
            this.btn_Unregister.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Unregister.ImageOptions.Image")));
            this.btn_Unregister.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btn_Unregister.ImageOptions.LargeImage")));
            this.btn_Unregister.Name = "btn_Unregister";
            this.btn_Unregister.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btn_Register
            // 
            this.btn_Register.Caption = "Register";
            this.btn_Register.Id = 22;
            this.btn_Register.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Register.ImageOptions.Image")));
            this.btn_Register.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btn_Register.ImageOptions.LargeImage")));
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btn_FocusBrowser
            // 
            this.btn_FocusBrowser.Caption = "Focus";
            this.btn_FocusBrowser.Id = 25;
            this.btn_FocusBrowser.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_FocusBrowser.ImageOptions.SvgImage")));
            this.btn_FocusBrowser.Name = "btn_FocusBrowser";
            // 
            // barButtonItemRandom
            // 
            this.barButtonItemRandom.Caption = "Random";
            this.barButtonItemRandom.Id = 26;
            this.barButtonItemRandom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemRandom.ImageOptions.Image")));
            this.barButtonItemRandom.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemRandom.ImageOptions.LargeImage")));
            this.barButtonItemRandom.Name = "barButtonItemRandom";
            // 
            // barButtonItemSave
            // 
            this.barButtonItemSave.Caption = "Save Profile";
            this.barButtonItemSave.Id = 27;
            this.barButtonItemSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.ImageOptions.Image")));
            this.barButtonItemSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.ImageOptions.LargeImage")));
            this.barButtonItemSave.Name = "barButtonItemSave";
            // 
            // barButtonTongleSelectConfig
            // 
            this.barButtonTongleSelectConfig.Caption = "Tongle Select Config";
            this.barButtonTongleSelectConfig.Id = 28;
            this.barButtonTongleSelectConfig.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonTongleSelectConfig.ImageOptions.Image")));
            this.barButtonTongleSelectConfig.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonTongleSelectConfig.ImageOptions.LargeImage")));
            this.barButtonTongleSelectConfig.Name = "barButtonTongleSelectConfig";
            this.barButtonTongleSelectConfig.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.ActAsDropDown = true;
            this.barButtonItem8.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem8.Caption = "Change View";
            this.barButtonItem8.Id = 17;
            this.barButtonItem8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem8.ImageOptions.Image")));
            this.barButtonItem8.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem8.ImageOptions.LargeImage")));
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // barButtonProxy
            // 
            this.barButtonProxy.Caption = "Manage Proxy";
            this.barButtonProxy.Id = 29;
            this.barButtonProxy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonProxy.ImageOptions.Image")));
            this.barButtonProxy.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonProxy.ImageOptions.LargeImage")));
            this.barButtonProxy.Name = "barButtonProxy";
            // 
            // barToggleSwitch_ProxyEnable
            // 
            this.barToggleSwitch_ProxyEnable.Caption = "Enable";
            this.barToggleSwitch_ProxyEnable.Id = 30;
            this.barToggleSwitch_ProxyEnable.Name = "barToggleSwitch_ProxyEnable";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "License";
            this.barButtonItem1.Id = 31;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btn_RandomRun
            // 
            this.btn_RandomRun.Caption = "Random Run";
            this.btn_RandomRun.Id = 32;
            this.btn_RandomRun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_RandomRun.ImageOptions.SvgImage")));
            this.btn_RandomRun.Name = "btn_RandomRun";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgProfile,
            this.rpgManaProfile,
            this.rpgBrowserAction,
            this.rpgNativeHost,
            this.rbgView,
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // rpgProfile
            // 
            this.rpgProfile.ItemLinks.Add(this.bbtnDeleteProfile);
            this.rpgProfile.ItemLinks.Add(this.bbtnEditProfile);
            this.rpgProfile.ItemLinks.Add(this.btnNew);
            this.rpgProfile.Name = "rpgProfile";
            this.rpgProfile.Text = "Profile";
            // 
            // rpgManaProfile
            // 
            this.rpgManaProfile.ItemLinks.Add(this.barButtonTongleSelectConfig);
            this.rpgManaProfile.ItemLinks.Add(this.barButtonItemRandom);
            this.rpgManaProfile.ItemLinks.Add(this.barButtonItemSave);
            this.rpgManaProfile.Name = "rpgManaProfile";
            this.rpgManaProfile.Text = "Manage Profile";
            // 
            // rpgBrowserAction
            // 
            this.rpgBrowserAction.ItemLinks.Add(this.btn_FocusBrowser);
            this.rpgBrowserAction.ItemLinks.Add(this.btn_RandomRun);
            this.rpgBrowserAction.Name = "rpgBrowserAction";
            this.rpgBrowserAction.Text = "Browser Actions";
            // 
            // rpgNativeHost
            // 
            this.rpgNativeHost.ItemLinks.Add(this.btn_Register);
            this.rpgNativeHost.ItemLinks.Add(this.btn_Unregister);
            this.rpgNativeHost.Name = "rpgNativeHost";
            this.rpgNativeHost.Text = "Native Host";
            this.rpgNativeHost.Visible = false;
            // 
            // rbgView
            // 
            this.rbgView.ItemLinks.Add(this.barButtonItem8);
            this.rbgView.Name = "rbgView";
            this.rbgView.Text = "View";
            this.rbgView.Visible = false;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonProxy);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Proxy";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "License";
            this.ribbonPageGroup2.Visible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 150);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(961, 455);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(961, 455);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(941, 435);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // toastNotificationsManager1
            // 
            this.toastNotificationsManager1.ApplicationId = "e43001e1-b3ad-4e18-91fb-ede621a41c94";
            // 
            // EmailListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "EmailListView";
            this.Size = new System.Drawing.Size(961, 605);
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem bbtnDeleteProfile;
        private DevExpress.XtraBars.BarButtonItem bbtnEditProfile;
        private DevExpress.XtraBars.BarButtonItem btn_Unregister;
        private DevExpress.XtraBars.BarButtonItem btn_Register;
        private DevExpress.XtraBars.BarButtonItem btn_FocusBrowser;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRandom;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonTongleSelectConfig;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonProxy;
        private DevExpress.XtraBars.BarToggleSwitchItem barToggleSwitch_ProxyEnable;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btn_RandomRun;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgProfile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgManaProfile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgBrowserAction;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgNativeHost;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rbgView;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView deviceGridView;
        private DevExpress.XtraGrid.Views.Grid.GridView emailGridView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn EmailName;
        private DevExpress.XtraGrid.Columns.GridColumn EmailAccount;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager1;
    }
}
