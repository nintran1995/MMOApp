namespace ZChangerMMO
{
	partial class frmSplashDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose ( );
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ( ) {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplashDialog));
            this.progress = new DevExpress.XtraEditors.ProgressBarControl();
            this.status = new DevExpress.XtraEditors.LabelControl();
            this.version = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(115, 103);
            this.progress.Name = "progress";
            this.progress.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            this.progress.Properties.Appearance.ForeColor = System.Drawing.Color.PaleGreen;
            this.progress.Size = new System.Drawing.Size(287, 9);
            this.progress.TabIndex = 2;
            // 
            // status
            // 
            this.status.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.status.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Appearance.ForeColor = System.Drawing.SystemColors.Window;
            this.status.Appearance.Options.UseBackColor = true;
            this.status.Appearance.Options.UseFont = true;
            this.status.Appearance.Options.UseForeColor = true;
            this.status.Location = new System.Drawing.Point(115, 84);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(152, 17);
            this.status.TabIndex = 3;
            this.status.Text = "Starting ZChanger Profile";
            // 
            // version
            // 
            this.version.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.version.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.Appearance.ForeColor = System.Drawing.SystemColors.Window;
            this.version.Appearance.Options.UseBackColor = true;
            this.version.Appearance.Options.UseFont = true;
            this.version.Appearance.Options.UseForeColor = true;
            this.version.Appearance.Options.UseTextOptions = true;
            this.version.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.version.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.version.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.version.Location = new System.Drawing.Point(115, 39);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(147, 23);
            this.version.TabIndex = 4;
            this.version.Text = "[Version]";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.Window;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(113, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(275, 23);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Vilas ZChanger MMO Tool";
            // 
            // frmSplashDialog
            // 
            this.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Center;
            this.BackgroundImageStore = global::ZChangerMMO.Properties.Resources.fingerprint;
            this.ClientSize = new System.Drawing.Size(620, 315);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.version);
            this.Controls.Add(this.status);
            this.Controls.Add(this.progress);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmSplashDialog.IconOptions.Icon")));
            this.Name = "frmSplashDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZChanger Pro";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private DevExpress.XtraEditors.ProgressBarControl progress;
		private DevExpress.XtraEditors.LabelControl status;
		private DevExpress.XtraEditors.LabelControl version;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}