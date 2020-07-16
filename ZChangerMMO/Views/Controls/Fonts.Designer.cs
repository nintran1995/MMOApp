namespace ZChangerMMO.Views.Controls
{
    partial class Fonts
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
            this.groupControlFonts = new DevExpress.XtraEditors.GroupControl();
            this.radioGroupFonts = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlFonts)).BeginInit();
            this.groupControlFonts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupFonts.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlFonts
            // 
            this.groupControlFonts.Controls.Add(this.radioGroupFonts);
            this.groupControlFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlFonts.Location = new System.Drawing.Point(0, 0);
            this.groupControlFonts.Name = "groupControlFonts";
            this.groupControlFonts.Size = new System.Drawing.Size(406, 202);
            this.groupControlFonts.TabIndex = 0;
            this.groupControlFonts.Text = "Fonts";
            // 
            // radioGroupFonts
            // 
            this.radioGroupFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroupFonts.Location = new System.Drawing.Point(2, 23);
            this.radioGroupFonts.Name = "radioGroupFonts";
            this.radioGroupFonts.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Windown 10"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Mac OS"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Windows 7"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "Linux"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(5, "Basic List 1"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(6, "Basic List 2"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(7, "Randomize"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(8, "No Fonts")});
            this.radioGroupFonts.Size = new System.Drawing.Size(402, 177);
            this.radioGroupFonts.TabIndex = 0;
            // 
            // Fonts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControlFonts);
            this.Name = "Fonts";
            this.Size = new System.Drawing.Size(406, 202);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlFonts)).EndInit();
            this.groupControlFonts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupFonts.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlFonts;
        public DevExpress.XtraEditors.RadioGroup radioGroupFonts;
    }
}
