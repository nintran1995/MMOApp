namespace ZChangerMMO.Views.Controls
{
    partial class TestProxies
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
            this.groupControlBulkTestProxies = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_TestProxy = new DevExpress.XtraEditors.SimpleButton();
            this.txt_Url = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.progressPanel_Testing = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBulkTestProxies)).BeginInit();
            this.groupControlBulkTestProxies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Url.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlBulkTestProxies
            // 
            this.groupControlBulkTestProxies.Controls.Add(this.layoutControl1);
            this.groupControlBulkTestProxies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlBulkTestProxies.Location = new System.Drawing.Point(0, 0);
            this.groupControlBulkTestProxies.Name = "groupControlBulkTestProxies";
            this.groupControlBulkTestProxies.Size = new System.Drawing.Size(631, 101);
            this.groupControlBulkTestProxies.TabIndex = 0;
            this.groupControlBulkTestProxies.Text = "Bulk Test Proxies";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_TestProxy);
            this.layoutControl1.Controls.Add(this.txt_Url);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 23);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(627, 76);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_TestProxy
            // 
            this.btn_TestProxy.Location = new System.Drawing.Point(514, 29);
            this.btn_TestProxy.Name = "btn_TestProxy";
            this.btn_TestProxy.Size = new System.Drawing.Size(101, 22);
            this.btn_TestProxy.StyleController = this.layoutControl1;
            this.btn_TestProxy.TabIndex = 6;
            this.btn_TestProxy.Text = "Test";
            this.btn_TestProxy.Click += new System.EventHandler(this.btn_TestProxy_Click);
            // 
            // txt_Url
            // 
            this.txt_Url.Location = new System.Drawing.Point(12, 29);
            this.txt_Url.Name = "txt_Url";
            this.txt_Url.Size = new System.Drawing.Size(484, 20);
            this.txt_Url.StyleController = this.layoutControl1;
            this.txt_Url.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(508, 13);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Enter the URL of a site you want to use your proxies on to test whether or not th" +
    "ey will work on that site.";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(627, 76);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(607, 17);
            this.layoutControlItem1.Text = "En";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txt_Url;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 17);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(488, 39);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_TestProxy;
            this.layoutControlItem3.Location = new System.Drawing.Point(502, 17);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(105, 39);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(488, 17);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(14, 39);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // progressPanel_Testing
            // 
            this.progressPanel_Testing.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel_Testing.Appearance.Options.UseBackColor = true;
            this.progressPanel_Testing.BarAnimationElementThickness = 2;
            this.progressPanel_Testing.Location = new System.Drawing.Point(325, 4);
            this.progressPanel_Testing.Name = "progressPanel_Testing";
            this.progressPanel_Testing.Size = new System.Drawing.Size(303, 91);
            this.progressPanel_Testing.TabIndex = 1;
            // 
            // TestProxies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControlBulkTestProxies);
            this.Controls.Add(this.progressPanel_Testing);
            this.Name = "TestProxies";
            this.Size = new System.Drawing.Size(631, 101);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBulkTestProxies)).EndInit();
            this.groupControlBulkTestProxies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Url.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlBulkTestProxies;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        public DevExpress.XtraEditors.SimpleButton btn_TestProxy;
        public DevExpress.XtraEditors.TextEdit txt_Url;
        public DevExpress.XtraWaitForm.ProgressPanel progressPanel_Testing;
    }
}
