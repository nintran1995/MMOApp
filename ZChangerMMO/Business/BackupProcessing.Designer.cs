namespace ZChangerMMO.BackupAndRestore
{
    partial class BackupProcessing
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
            this.backup_ProgressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.backup_ProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.lbl_backupProcessing_status = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.backup_ProgressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // backup_ProgressPanel
            // 
            this.backup_ProgressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.backup_ProgressPanel.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.backup_ProgressPanel.Appearance.Options.UseBackColor = true;
            this.backup_ProgressPanel.Appearance.Options.UseForeColor = true;
            this.backup_ProgressPanel.BarAnimationElementThickness = 2;
            this.backup_ProgressPanel.Location = new System.Drawing.Point(54, 40);
            this.backup_ProgressPanel.Name = "backup_ProgressPanel";
            this.backup_ProgressPanel.Size = new System.Drawing.Size(246, 66);
            this.backup_ProgressPanel.TabIndex = 0;
            this.backup_ProgressPanel.Text = "progressPanel1";
            // 
            // backup_ProgressBar
            // 
            this.backup_ProgressBar.Location = new System.Drawing.Point(54, 185);
            this.backup_ProgressBar.Name = "backup_ProgressBar";
            this.backup_ProgressBar.Size = new System.Drawing.Size(679, 18);
            this.backup_ProgressBar.TabIndex = 1;
            // 
            // lbl_backupProcessing_status
            // 
            this.lbl_backupProcessing_status.Location = new System.Drawing.Point(54, 236);
            this.lbl_backupProcessing_status.Name = "lbl_backupProcessing_status";
            this.lbl_backupProcessing_status.Size = new System.Drawing.Size(0, 13);
            this.lbl_backupProcessing_status.TabIndex = 2;
            // 
            // BackupProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backup_ProgressPanel);
            this.Controls.Add(this.lbl_backupProcessing_status);
            this.Controls.Add(this.backup_ProgressBar);
            this.Name = "BackupProcessing";
            this.Size = new System.Drawing.Size(767, 302);
            ((System.ComponentModel.ISupportInitialize)(this.backup_ProgressBar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public DevExpress.XtraWaitForm.ProgressPanel backup_ProgressPanel;
        public DevExpress.XtraEditors.LabelControl lbl_backupProcessing_status;
        public DevExpress.XtraEditors.ProgressBarControl backup_ProgressBar;
    }
}
