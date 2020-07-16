namespace ZChangerMMO.Views.Controls
{
    partial class ProxyList
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
            this.groupControlProxyList = new DevExpress.XtraEditors.GroupControl();
            this.grid_Proxy = new DevExpress.XtraGrid.GridControl();
            this.gridView_Proxy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grid_Proxy_NameCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid_Proxy_HostCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid_Proxy_PortCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid_Proxy_UserNameCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid_Proxy_PasswordCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grid_Proxy_EnabledCol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlActions = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Enable = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Disable = new DevExpress.XtraEditors.SimpleButton();
            this.btn_remove = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlBulkSelect = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btn_SelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ClearSelection = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.grid_Proxy_SchemeCol = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlProxyList)).BeginInit();
            this.groupControlProxyList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Proxy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Proxy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlActions)).BeginInit();
            this.layoutControlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBulkSelect)).BeginInit();
            this.layoutControlBulkSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlProxyList
            // 
            this.groupControlProxyList.Controls.Add(this.grid_Proxy);
            this.groupControlProxyList.Controls.Add(this.layoutControlActions);
            this.groupControlProxyList.Controls.Add(this.layoutControlBulkSelect);
            this.groupControlProxyList.Controls.Add(this.labelControl1);
            this.groupControlProxyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlProxyList.Location = new System.Drawing.Point(0, 0);
            this.groupControlProxyList.Margin = new System.Windows.Forms.Padding(0);
            this.groupControlProxyList.Name = "groupControlProxyList";
            this.groupControlProxyList.Size = new System.Drawing.Size(989, 512);
            this.groupControlProxyList.TabIndex = 0;
            this.groupControlProxyList.Text = "Proxy List";
            // 
            // grid_Proxy
            // 
            this.grid_Proxy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_Proxy.Location = new System.Drawing.Point(9, 136);
            this.grid_Proxy.MainView = this.gridView_Proxy;
            this.grid_Proxy.Name = "grid_Proxy";
            this.grid_Proxy.Size = new System.Drawing.Size(975, 371);
            this.grid_Proxy.TabIndex = 9;
            this.grid_Proxy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Proxy});
            // 
            // gridView_Proxy
            // 
            this.gridView_Proxy.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grid_Proxy_NameCol,
            this.grid_Proxy_HostCol,
            this.grid_Proxy_PortCol,
            this.grid_Proxy_UserNameCol,
            this.grid_Proxy_PasswordCol,
            this.grid_Proxy_EnabledCol,
            this.grid_Proxy_SchemeCol});
            this.gridView_Proxy.GridControl = this.grid_Proxy;
            this.gridView_Proxy.Name = "gridView_Proxy";
            this.gridView_Proxy.OptionsSelection.MultiSelect = true;
            this.gridView_Proxy.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_Proxy_CellValueChanged);
            // 
            // grid_Proxy_NameCol
            // 
            this.grid_Proxy_NameCol.Caption = "Name";
            this.grid_Proxy_NameCol.FieldName = "Name";
            this.grid_Proxy_NameCol.Name = "grid_Proxy_NameCol";
            this.grid_Proxy_NameCol.Visible = true;
            this.grid_Proxy_NameCol.VisibleIndex = 0;
            // 
            // grid_Proxy_HostCol
            // 
            this.grid_Proxy_HostCol.Caption = "Host";
            this.grid_Proxy_HostCol.FieldName = "Host";
            this.grid_Proxy_HostCol.Name = "grid_Proxy_HostCol";
            this.grid_Proxy_HostCol.Visible = true;
            this.grid_Proxy_HostCol.VisibleIndex = 1;
            // 
            // grid_Proxy_PortCol
            // 
            this.grid_Proxy_PortCol.Caption = "Port";
            this.grid_Proxy_PortCol.FieldName = "Port";
            this.grid_Proxy_PortCol.Name = "grid_Proxy_PortCol";
            this.grid_Proxy_PortCol.Visible = true;
            this.grid_Proxy_PortCol.VisibleIndex = 2;
            // 
            // grid_Proxy_UserNameCol
            // 
            this.grid_Proxy_UserNameCol.Caption = "UserName";
            this.grid_Proxy_UserNameCol.FieldName = "UserName";
            this.grid_Proxy_UserNameCol.Name = "grid_Proxy_UserNameCol";
            this.grid_Proxy_UserNameCol.Visible = true;
            this.grid_Proxy_UserNameCol.VisibleIndex = 4;
            // 
            // grid_Proxy_PasswordCol
            // 
            this.grid_Proxy_PasswordCol.Caption = "Password";
            this.grid_Proxy_PasswordCol.FieldName = "Password";
            this.grid_Proxy_PasswordCol.Name = "grid_Proxy_PasswordCol";
            this.grid_Proxy_PasswordCol.Visible = true;
            this.grid_Proxy_PasswordCol.VisibleIndex = 5;
            // 
            // grid_Proxy_EnabledCol
            // 
            this.grid_Proxy_EnabledCol.Caption = "Enabled";
            this.grid_Proxy_EnabledCol.FieldName = "Enabled";
            this.grid_Proxy_EnabledCol.Name = "grid_Proxy_EnabledCol";
            this.grid_Proxy_EnabledCol.Visible = true;
            this.grid_Proxy_EnabledCol.VisibleIndex = 6;
            // 
            // layoutControlActions
            // 
            this.layoutControlActions.Controls.Add(this.labelControl2);
            this.layoutControlActions.Controls.Add(this.btn_Enable);
            this.layoutControlActions.Controls.Add(this.btn_Disable);
            this.layoutControlActions.Controls.Add(this.btn_remove);
            this.layoutControlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControlActions.Location = new System.Drawing.Point(2, 94);
            this.layoutControlActions.Name = "layoutControlActions";
            this.layoutControlActions.Root = this.Root;
            this.layoutControlActions.Size = new System.Drawing.Size(985, 36);
            this.layoutControlActions.TabIndex = 1;
            this.layoutControlActions.Text = "layoutControl1";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(4, 0, 34, 0);
            this.labelControl2.Size = new System.Drawing.Size(77, 13);
            this.labelControl2.StyleController = this.layoutControlActions;
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Actions:";
            // 
            // btn_Enable
            // 
            this.btn_Enable.Location = new System.Drawing.Point(88, 7);
            this.btn_Enable.Name = "btn_Enable";
            this.btn_Enable.Size = new System.Drawing.Size(87, 22);
            this.btn_Enable.StyleController = this.layoutControlActions;
            this.btn_Enable.TabIndex = 6;
            this.btn_Enable.Text = "Enable";
            this.btn_Enable.Click += new System.EventHandler(this.btn_Enable_Click);
            // 
            // btn_Disable
            // 
            this.btn_Disable.Location = new System.Drawing.Point(190, 7);
            this.btn_Disable.Name = "btn_Disable";
            this.btn_Disable.Size = new System.Drawing.Size(87, 22);
            this.btn_Disable.StyleController = this.layoutControlActions;
            this.btn_Disable.TabIndex = 5;
            this.btn_Disable.Text = "Disable";
            this.btn_Disable.Click += new System.EventHandler(this.btn_Disable_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(292, 7);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(87, 22);
            this.btn_remove.StyleController = this.layoutControlActions;
            this.btn_remove.TabIndex = 4;
            this.btn_remove.Text = "Remove";
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(985, 36);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_remove;
            this.layoutControlItem1.Location = new System.Drawing.Point(285, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_Disable;
            this.layoutControlItem2.Location = new System.Drawing.Point(183, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Enable;
            this.layoutControlItem3.Location = new System.Drawing.Point(81, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(376, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(599, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(172, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(11, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem4.Location = new System.Drawing.Point(274, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(11, 26);
            this.emptySpaceItem4.Text = "emptySpaceItem2";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlBulkSelect
            // 
            this.layoutControlBulkSelect.Controls.Add(this.labelControl3);
            this.layoutControlBulkSelect.Controls.Add(this.btn_SelectAll);
            this.layoutControlBulkSelect.Controls.Add(this.btn_ClearSelection);
            this.layoutControlBulkSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControlBulkSelect.Location = new System.Drawing.Point(2, 56);
            this.layoutControlBulkSelect.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControlBulkSelect.Name = "layoutControlBulkSelect";
            this.layoutControlBulkSelect.Root = this.layoutControlGroup1;
            this.layoutControlBulkSelect.Size = new System.Drawing.Size(985, 38);
            this.layoutControlBulkSelect.TabIndex = 8;
            this.layoutControlBulkSelect.Text = "layoutControl1";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 7);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.labelControl3.Size = new System.Drawing.Size(75, 13);
            this.labelControl3.StyleController = this.layoutControlBulkSelect;
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Bulk Select:";
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Location = new System.Drawing.Point(88, 7);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(87, 22);
            this.btn_SelectAll.StyleController = this.layoutControlBulkSelect;
            this.btn_SelectAll.TabIndex = 6;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.Click += new System.EventHandler(this.btn_SelectAll_Click);
            // 
            // btn_ClearSelection
            // 
            this.btn_ClearSelection.Location = new System.Drawing.Point(190, 7);
            this.btn_ClearSelection.Name = "btn_ClearSelection";
            this.btn_ClearSelection.Size = new System.Drawing.Size(90, 22);
            this.btn_ClearSelection.StyleController = this.layoutControlBulkSelect;
            this.btn_ClearSelection.TabIndex = 5;
            this.btn_ClearSelection.Text = "Clear Selection";
            this.btn_ClearSelection.Click += new System.EventHandler(this.btn_ClearSelection_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem1,
            this.emptySpaceItem5,
            this.emptySpaceItem6});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(985, 38);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_ClearSelection;
            this.layoutControlItem6.Location = new System.Drawing.Point(183, 0);
            this.layoutControlItem6.Name = "layoutControlItem2";
            this.layoutControlItem6.Size = new System.Drawing.Size(94, 28);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_SelectAll;
            this.layoutControlItem7.Location = new System.Drawing.Point(81, 0);
            this.layoutControlItem7.Name = "layoutControlItem3";
            this.layoutControlItem7.Size = new System.Drawing.Size(91, 28);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl3;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem4";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 2, 2, 2);
            this.layoutControlItem8.Size = new System.Drawing.Size(81, 28);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(288, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem3";
            this.emptySpaceItem1.Size = new System.Drawing.Size(687, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(172, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem2";
            this.emptySpaceItem5.Size = new System.Drawing.Size(11, 28);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem6.Location = new System.Drawing.Point(277, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem4";
            this.emptySpaceItem6.Size = new System.Drawing.Size(11, 28);
            this.emptySpaceItem6.Text = "emptySpaceItem2";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(2, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(12, 10, 0, 10);
            this.labelControl1.Size = new System.Drawing.Size(263, 33);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Select proxies by tag to bulk enable or disable them.";
            // 
            // grid_Proxy_SchemeCol
            // 
            this.grid_Proxy_SchemeCol.Caption = "Scheme";
            this.grid_Proxy_SchemeCol.FieldName = "Scheme";
            this.grid_Proxy_SchemeCol.Name = "grid_Proxy_SchemeCol";
            this.grid_Proxy_SchemeCol.Visible = true;
            this.grid_Proxy_SchemeCol.VisibleIndex = 3;
            // 
            // ProxyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControlProxyList);
            this.Name = "ProxyList";
            this.Size = new System.Drawing.Size(989, 512);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlProxyList)).EndInit();
            this.groupControlProxyList.ResumeLayout(false);
            this.groupControlProxyList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Proxy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Proxy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlActions)).EndInit();
            this.layoutControlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBulkSelect)).EndInit();
            this.layoutControlBulkSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlProxyList;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControlActions;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Enable;
        private DevExpress.XtraEditors.SimpleButton btn_Disable;
        private DevExpress.XtraEditors.SimpleButton btn_remove;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControl layoutControlBulkSelect;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_SelectAll;
        private DevExpress.XtraEditors.SimpleButton btn_ClearSelection;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Proxy;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_NameCol;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_HostCol;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_PortCol;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_UserNameCol;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_PasswordCol;
        public DevExpress.XtraGrid.GridControl grid_Proxy;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_EnabledCol;
        private DevExpress.XtraGrid.Columns.GridColumn grid_Proxy_SchemeCol;
    }
}
