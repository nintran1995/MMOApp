namespace ZChangerMMO.Views.Controls
{
    partial class DeviceModel
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
            this.groupControlContent = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxEditDeviceModel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkEditDeviceModel = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlContent)).BeginInit();
            this.groupControlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDeviceModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDeviceModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlContent
            // 
            this.groupControlContent.Controls.Add(this.layoutControl1);
            this.groupControlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlContent.Location = new System.Drawing.Point(0, 0);
            this.groupControlContent.Name = "groupControlContent";
            this.groupControlContent.Size = new System.Drawing.Size(671, 99);
            this.groupControlContent.TabIndex = 1;
            this.groupControlContent.Text = "Device Model";
            this.groupControlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControlContent_Paint);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.comboBoxEditDeviceModel);
            this.layoutControl1.Controls.Add(this.checkEditDeviceModel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 23);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(667, 74);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // comboBoxEditDeviceModel
            // 
            this.comboBoxEditDeviceModel.Location = new System.Drawing.Point(224, 12);
            this.comboBoxEditDeviceModel.Name = "comboBoxEditDeviceModel";
            this.comboBoxEditDeviceModel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditDeviceModel.Properties.Items.AddRange(new object[] {
            "WindowsPC",
            "Macintosh",
            "iPhone",
            "iPad",
            "Samsung",
            "Xiaomi",
            "Oppo"});
            this.comboBoxEditDeviceModel.Size = new System.Drawing.Size(431, 20);
            this.comboBoxEditDeviceModel.StyleController = this.layoutControl1;
            this.comboBoxEditDeviceModel.TabIndex = 5;
            // 
            // checkEditDeviceModel
            // 
            this.checkEditDeviceModel.Location = new System.Drawing.Point(12, 12);
            this.checkEditDeviceModel.Name = "checkEditDeviceModel";
            this.checkEditDeviceModel.Properties.Caption = "Device Model";
            this.checkEditDeviceModel.Size = new System.Drawing.Size(208, 20);
            this.checkEditDeviceModel.StyleController = this.layoutControl1;
            this.checkEditDeviceModel.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(667, 74);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.checkEditDeviceModel;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(212, 54);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEditDeviceModel;
            this.layoutControlItem2.Location = new System.Drawing.Point(212, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(435, 54);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // DeviceModel
            // 
            this.Controls.Add(this.groupControlContent);
            this.Name = "DeviceModel";
            this.Size = new System.Drawing.Size(671, 99);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlContent)).EndInit();
            this.groupControlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDeviceModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDeviceModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlContent;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDeviceModel;
        public DevExpress.XtraEditors.CheckEdit checkEditDeviceModel;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
