namespace ZChangerMMO.Views
{
    partial class EmailAccountView
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
            this.deviceGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDeviceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeviceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.emailGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNEmailame = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmailAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bbiUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.bbiUpdateDataBindings = new DevExpress.XtraBars.BarButtonItem();
            this.bbiUpdateCommands = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteEntity = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExpandEntity = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCollapseEntity = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // deviceGridView
            // 
            this.deviceGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDeviceID,
            this.colDeviceName});
            this.deviceGridView.GridControl = this.gridControl1;
            this.deviceGridView.Name = "deviceGridView";
            // 
            // colDeviceID
            // 
            this.colDeviceID.Caption = "ID";
            this.colDeviceID.FieldName = "ID";
            this.colDeviceID.Name = "colDeviceID";
            this.colDeviceID.Visible = true;
            this.colDeviceID.VisibleIndex = 1;
            // 
            // colDeviceName
            // 
            this.colDeviceName.Caption = "Name";
            this.colDeviceName.FieldName = "Name";
            this.colDeviceName.Name = "colDeviceName";
            this.colDeviceName.Visible = true;
            this.colDeviceName.VisibleIndex = 0;
            // 
            // gridControl1
            // 
            gridLevelNode1.LevelTemplate = this.deviceGridView;
            gridLevelNode1.RelationName = "Devices";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.emailGridView;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(737, 490);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.emailGridView,
            this.deviceGridView});
            // 
            // emailGridView
            // 
            this.emailGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmailID,
            this.colNEmailame,
            this.colEmailAccount});
            this.emailGridView.GridControl = this.gridControl1;
            this.emailGridView.Name = "emailGridView";
            // 
            // colEmailID
            // 
            this.colEmailID.Caption = "ID";
            this.colEmailID.FieldName = "ID";
            this.colEmailID.Name = "colEmailID";
            this.colEmailID.Visible = true;
            this.colEmailID.VisibleIndex = 2;
            // 
            // colNEmailame
            // 
            this.colNEmailame.Caption = "Name";
            this.colNEmailame.FieldName = "Name";
            this.colNEmailame.Name = "colNEmailame";
            this.colNEmailame.Visible = true;
            this.colNEmailame.VisibleIndex = 0;
            // 
            // colEmailAccount
            // 
            this.colEmailAccount.Caption = "Email";
            this.colEmailAccount.FieldName = "EmailAccount";
            this.colEmailAccount.Name = "colEmailAccount";
            this.colEmailAccount.Visible = true;
            this.colEmailAccount.VisibleIndex = 1;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.BindingExpressions.AddRange(new DevExpress.Utils.MVVM.BindingExpression[] {
            DevExpress.Utils.MVVM.BindingExpression.CreateParameterizedCommandBinding(typeof(ZChangerMMO.ViewModels.EmailAccountViewModel), "Update", null, this.bbiUpdate),
            DevExpress.Utils.MVVM.BindingExpression.CreateCommandBinding(typeof(ZChangerMMO.ViewModels.EmailAccountViewModel), "UpdateDataBindings", this.bbiUpdateDataBindings),
            DevExpress.Utils.MVVM.BindingExpression.CreateParameterizedCommandBinding(typeof(ZChangerMMO.ViewModels.EmailAccountViewModel), "UpdateCommands", null, this.bbiUpdateCommands),
            DevExpress.Utils.MVVM.BindingExpression.CreateParameterizedCommandBinding(typeof(ZChangerMMO.ViewModels.EmailAccountViewModel), "DeleteEntity", null, this.bbiDeleteEntity),
            DevExpress.Utils.MVVM.BindingExpression.CreateParameterizedCommandBinding(typeof(ZChangerMMO.ViewModels.EmailAccountViewModel), "ExpandEntity", null, this.bbiExpandEntity),
            DevExpress.Utils.MVVM.BindingExpression.CreateParameterizedCommandBinding(typeof(ZChangerMMO.ViewModels.EmailAccountViewModel), "CollapseEntity", null, this.bbiCollapseEntity)});
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(ZChangerMMO.ViewModels.EmailAccountViewModel);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.bbiUpdate,
            this.bbiUpdateDataBindings,
            this.bbiUpdateCommands,
            this.bbiDeleteEntity,
            this.bbiExpandEntity,
            this.bbiCollapseEntity});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(761, 150);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 150);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(761, 514);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(761, 514);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(741, 494);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // bbiUpdate
            // 
            this.bbiUpdate.Caption = "Update";
            this.bbiUpdate.Id = 1;
            this.bbiUpdate.ImageOptions.ImageUri.Uri = "Update";
            this.bbiUpdate.Name = "bbiUpdate";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiUpdate);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiUpdateDataBindings);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiUpdateCommands);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiDeleteEntity);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiExpandEntity);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiCollapseEntity);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // bbiUpdateDataBindings
            // 
            this.bbiUpdateDataBindings.Caption = "UpdateDataBindings";
            this.bbiUpdateDataBindings.Id = 2;
            this.bbiUpdateDataBindings.ImageOptions.ImageUri.Uri = "UpdateDataBindings";
            this.bbiUpdateDataBindings.Name = "bbiUpdateDataBindings";
            // 
            // bbiUpdateCommands
            // 
            this.bbiUpdateCommands.Caption = "UpdateCommands";
            this.bbiUpdateCommands.Id = 3;
            this.bbiUpdateCommands.ImageOptions.ImageUri.Uri = "UpdateCommands";
            this.bbiUpdateCommands.Name = "bbiUpdateCommands";
            // 
            // bbiDeleteEntity
            // 
            this.bbiDeleteEntity.Caption = "DeleteEntity";
            this.bbiDeleteEntity.Id = 4;
            this.bbiDeleteEntity.ImageOptions.ImageUri.Uri = "DeleteEntity";
            this.bbiDeleteEntity.Name = "bbiDeleteEntity";
            // 
            // bbiExpandEntity
            // 
            this.bbiExpandEntity.Caption = "ExpandEntity";
            this.bbiExpandEntity.Id = 5;
            this.bbiExpandEntity.ImageOptions.ImageUri.Uri = "ExpandEntity";
            this.bbiExpandEntity.Name = "bbiExpandEntity";
            // 
            // bbiCollapseEntity
            // 
            this.bbiCollapseEntity.Caption = "CollapseEntity";
            this.bbiCollapseEntity.Id = 6;
            this.bbiCollapseEntity.ImageOptions.ImageUri.Uri = "CollapseEntity";
            this.bbiCollapseEntity.Name = "bbiCollapseEntity";
            // 
            // EmailAccountView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "EmailAccountView";
            this.Size = new System.Drawing.Size(761, 664);
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView emailGridView;
        private DevExpress.XtraGrid.Views.Grid.GridView deviceGridView;
        private DevExpress.XtraGrid.Columns.GridColumn colNEmailame;
        private DevExpress.XtraGrid.Columns.GridColumn colEmailAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colEmailID;
        private DevExpress.XtraGrid.Columns.GridColumn colDeviceID;
        private DevExpress.XtraGrid.Columns.GridColumn colDeviceName;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem bbiUpdate;
        private DevExpress.XtraBars.BarButtonItem bbiUpdateDataBindings;
        private DevExpress.XtraBars.BarButtonItem bbiUpdateCommands;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteEntity;
        private DevExpress.XtraBars.BarButtonItem bbiExpandEntity;
        private DevExpress.XtraBars.BarButtonItem bbiCollapseEntity;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
    }
}
