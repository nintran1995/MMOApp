namespace ZChangerMMO.Business
{
    partial class BackupSettings
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
            this.lbl_BackupItemName = new DevExpress.XtraEditors.LabelControl();
            this.txt_BackupName = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridControl_BackupItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSize = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txt_BackupName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_BackupItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_BackupItemName
            // 
            this.lbl_BackupItemName.Location = new System.Drawing.Point(13, 20);
            this.lbl_BackupItemName.Name = "lbl_BackupItemName";
            this.lbl_BackupItemName.Size = new System.Drawing.Size(27, 13);
            this.lbl_BackupItemName.TabIndex = 0;
            this.lbl_BackupItemName.Text = "Name";
            // 
            // txt_BackupName
            // 
            this.txt_BackupName.Location = new System.Drawing.Point(66, 17);
            this.txt_BackupName.Name = "txt_BackupName";
            this.txt_BackupName.Size = new System.Drawing.Size(709, 20);
            this.txt_BackupName.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(13, 63);
            this.gridControl1.MainView = this.gridControl_BackupItems;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(762, 323);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl_BackupItems});
            // 
            // gridControl_BackupItems
            // 
            this.gridControl_BackupItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnSize});
            this.gridControl_BackupItems.GridControl = this.gridControl1;
            this.gridControl_BackupItems.Name = "gridControl_BackupItems";
            this.gridControl_BackupItems.OptionsSelection.MultiSelect = true;
            this.gridControl_BackupItems.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            // 
            // gridColumnSize
            // 
            this.gridColumnSize.Caption = "Size";
            this.gridColumnSize.FieldName = "Size";
            this.gridColumnSize.Name = "gridColumnSize";
            this.gridColumnSize.Visible = true;
            this.gridColumnSize.VisibleIndex = 2;
            // 
            // BackupSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.txt_BackupName);
            this.Controls.Add(this.lbl_BackupItemName);
            this.Name = "BackupSettings";
            this.Size = new System.Drawing.Size(793, 466);
            ((System.ComponentModel.ISupportInitialize)(this.txt_BackupName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_BackupItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbl_BackupItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSize;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridControl_BackupItems;
        public DevExpress.XtraEditors.TextEdit txt_BackupName;
    }
}
