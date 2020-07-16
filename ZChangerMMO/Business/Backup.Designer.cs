namespace ZChangerMMO.BackupAndRestore
{
    partial class Backup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Backup));
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Backup = new DevExpress.XtraEditors.SimpleButton();
            this.work_sizesItems = new System.ComponentModel.BackgroundWorker();
            this.work_BackUp = new System.ComponentModel.BackgroundWorker();
            this.btn_Complete = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.backupCompleteUserControl = new ZChangerMMO.Business.BackupComplete();
            this.backupSettingsUserControl = new ZChangerMMO.Business.BackupSettings();
            this.backupProcessingUserControl = new ZChangerMMO.BackupAndRestore.BackupProcessing();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(170, 3);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Backup
            // 
            this.btn_Backup.Appearance.BackColor = System.Drawing.Color.Blue;
            this.btn_Backup.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Backup.Appearance.Options.UseBackColor = true;
            this.btn_Backup.Appearance.Options.UseForeColor = true;
            this.btn_Backup.Location = new System.Drawing.Point(89, 3);
            this.btn_Backup.Name = "btn_Backup";
            this.btn_Backup.Size = new System.Drawing.Size(75, 23);
            this.btn_Backup.TabIndex = 4;
            this.btn_Backup.Text = "Backup";
            this.btn_Backup.Click += new System.EventHandler(this.btn_Backup_Click);
            // 
            // work_sizesItems
            // 
            this.work_sizesItems.WorkerReportsProgress = true;
            this.work_sizesItems.WorkerSupportsCancellation = true;
            this.work_sizesItems.DoWork += new System.ComponentModel.DoWorkEventHandler(this.work_sizesItems_DoWork);
            this.work_sizesItems.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.work_sizesItems_RunWorkerCompleted);
            // 
            // work_BackUp
            // 
            this.work_BackUp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.work_UpdateBackupStatus_DoWork);
            this.work_BackUp.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.work_BackUp_RunWorkerCompleted);
            // 
            // btn_Complete
            // 
            this.btn_Complete.Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Complete.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btn_Complete.Appearance.BorderColor = System.Drawing.Color.White;
            this.btn_Complete.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Complete.Appearance.Options.UseBackColor = true;
            this.btn_Complete.Appearance.Options.UseBorderColor = true;
            this.btn_Complete.Appearance.Options.UseForeColor = true;
            this.btn_Complete.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.btn_Complete.AppearanceDisabled.Options.UseForeColor = true;
            this.btn_Complete.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btn_Complete.AppearanceHovered.Options.UseForeColor = true;
            this.btn_Complete.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btn_Complete.AppearancePressed.Options.UseForeColor = true;
            this.btn_Complete.Location = new System.Drawing.Point(8, 3);
            this.btn_Complete.Name = "btn_Complete";
            this.btn_Complete.Size = new System.Drawing.Size(75, 23);
            this.btn_Complete.TabIndex = 7;
            this.btn_Complete.Text = "Complete";
            this.btn_Complete.Click += new System.EventHandler(this.btn_Complete_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_Cancel);
            this.flowLayoutPanel1.Controls.Add(this.btn_Backup);
            this.flowLayoutPanel1.Controls.Add(this.btn_Complete);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(527, 413);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(248, 34);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // backupCompleteUserControl
            // 
            this.backupCompleteUserControl.Location = new System.Drawing.Point(13, 13);
            this.backupCompleteUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backupCompleteUserControl.Name = "backupCompleteUserControl";
            this.backupCompleteUserControl.Size = new System.Drawing.Size(775, 394);
            this.backupCompleteUserControl.TabIndex = 9;
            // 
            // backupSettingsUserControl
            // 
            this.backupSettingsUserControl.Location = new System.Drawing.Point(2, 1);
            this.backupSettingsUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backupSettingsUserControl.Name = "backupSettingsUserControl";
            this.backupSettingsUserControl.Size = new System.Drawing.Size(797, 395);
            this.backupSettingsUserControl.TabIndex = 6;
            // 
            // backupProcessingUserControl
            // 
            this.backupProcessingUserControl.Location = new System.Drawing.Point(13, 1);
            this.backupProcessingUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backupProcessingUserControl.Name = "backupProcessingUserControl";
            this.backupProcessingUserControl.Size = new System.Drawing.Size(775, 396);
            this.backupProcessingUserControl.TabIndex = 5;
            // 
            // Backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.backupCompleteUserControl);
            this.Controls.Add(this.backupSettingsUserControl);
            this.Controls.Add(this.backupProcessingUserControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Backup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup browser profile";
            this.Load += new System.EventHandler(this.Backup_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Backup;
        private System.ComponentModel.BackgroundWorker work_sizesItems;
        private System.ComponentModel.BackgroundWorker work_BackUp;
        private BackupProcessing backupProcessingUserControl;
        private Business.BackupSettings backupSettingsUserControl;
        private DevExpress.XtraEditors.SimpleButton btn_Complete;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Business.BackupComplete backupCompleteUserControl;
    }
}