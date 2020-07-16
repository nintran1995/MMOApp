using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ZChangerMMO.DataModels;
using ZChangerMMO.Events;

namespace ZChangerMMO.BackupAndRestore
{
    public enum BackupStep
    {
        BACKUP_SETTING,
        BACKUP_PROCESSING,
        BACKUP_COMPLETE
    }

    public partial class Backup : Form
    {
        public string[] DefaultBackupDataItems = new string[] { 
            "First Run",
            "Last Browser",
            "Last Version", 
            "Local State", 
            "Module Info Cache", 
            "Safe Browsing Cookies", 
            "Safe Browsing Cookies-journal" 
        };

        public string[] DefaultBackupList = new string[] { 
            "Extensions", 
            "Cookies", 
            "Current Session", 
            "Current Tabs", 
            "Login Data",
            "Preferences",
            "Secure Preferences",
            "Visited Links", 
            "Web Data", 
            "History"
        };

        public Backup(string sourceFolder, string destFolder, Profile profile)
        {
            BackupDataItems = GetBackupItems();
            CommonBackupDataItems = GetDefaultBackupDataItems();
            SourceBackupFolder = sourceFolder;
            DestBackupFolder = destFolder;
            BackupProcess = new BackupProcess(SourceBackupFolder, DestBackupFolder, profile);
            BackupProcess.BackUpProcessUpdate += BackUpProcessUpdateHandler;
            CurrentStep = BackupStep.BACKUP_SETTING;
            InitializeComponent();
        }

        List<BackupDataItem> GetSelectedItems()
        {
            List<BackupDataItem> result = new List<BackupDataItem>();
            int[] selectedRowIndexs = backupSettingsUserControl.gridControl_BackupItems.GetSelectedRows();
            foreach(int index in selectedRowIndexs)
            {
                result.Add(backupSettingsUserControl.gridControl_BackupItems.GetRow(index) as BackupDataItem);
            }
            return result;
        }

        void UpdateUIForStep(BackupStep step)
        {
            CurrentStep = step;
            switch(step)
            {
                case BackupStep.BACKUP_SETTING:
                    backupSettingsUserControl.Show();
                    backupProcessingUserControl.Hide();
                    backupCompleteUserControl.Hide();
                    btn_Backup.Show();
                    btn_Cancel.Show();
                    btn_Complete.Hide();
                    break;
                case BackupStep.BACKUP_PROCESSING:
                    backupSettingsUserControl.Hide();
                    backupProcessingUserControl.Show();
                    backupCompleteUserControl.Hide();
                    btn_Backup.Hide();
                    btn_Cancel.Hide();
                    btn_Complete.Hide();
                    break;
                case BackupStep.BACKUP_COMPLETE:
                    backupSettingsUserControl.Hide();
                    backupProcessingUserControl.Hide();
                    backupCompleteUserControl.Show();
                    btn_Backup.Hide();
                    btn_Cancel.Hide();
                    btn_Complete.Show();
                    break;
            }
        }

        public List<BackupDataItem> GetBackupItems()
        {
            List<BackupDataItem> items = new List<BackupDataItem>();
            foreach(string item in DefaultBackupList)
            {
                items.Add(new BackupDataItem { Name = item, Size = string.Empty, Type = string.Equals("Extensions", item) ? BackupDataItemType.FOLDER : BackupDataItemType.FILE, ItemLevel = ItemLevel.PROFILE });
            }

            return items;
        }

        public List<BackupDataItem> GetDefaultBackupDataItems()
        {
            List<BackupDataItem> list = new List<BackupDataItem>();

            foreach(string fileName in DefaultBackupDataItems)
            {
                list.Add(new BackupDataItem { Name = fileName, Size = string.Empty, Type = BackupDataItemType.FILE, ItemLevel = ItemLevel.USER });
            }

            return list;
        }

        #region Properties
        public BackgroundWorker Work_sizesItems => work_sizesItems;

        public string DestBackupFolder { get; set; }// = @"D:\backup_chrome";

        public string SourceBackupFolder { get; set; } //= @"C:\Users\Kien (Karl) T. TRINH\AppData\Local\Google\Chrome\User Data\Default";

        public List<BackupDataItem> BackupDataItems;
        public List<BackupDataItem> CommonBackupDataItems;
        BackupProcess BackupProcess;
        BackupStep CurrentStep;
        BackUpResult BackUpProcessResult;

        public event EventHandler<BackUpFormEventArgs> BackUpFormAction;

        #endregion


        #region Event handlers
        void Backup_Load(object sender, EventArgs e)
        {
            UpdateUIForStep(BackupStep.BACKUP_SETTING);
            backupSettingsUserControl.gridControl1.DataSource = BackupDataItems; //listBackupData.DataSource = BackupDataItems;
            backupSettingsUserControl.gridControl_BackupItems.SelectAll();
            work_sizesItems.RunWorkerAsync();
        }

        void btn_Backup_Click(object sender, EventArgs e)
        {
            UpdateUIForStep(BackupStep.BACKUP_PROCESSING);
            work_BackUp.RunWorkerAsync();
        }

        void btn_Cancel_Click(object sender, EventArgs e) => Close();

        void btn_Complete_Click(object sender, EventArgs e)
        {
            BackUpFormEventArgs eventData = new BackUpFormEventArgs
            {
                ActionType = BackUpFormActionType.FINISH_BACKUP,
                BackupName = backupSettingsUserControl.txt_BackupName.Text,
                FilePath = BackUpProcessResult.FileName,
                BackupTime = BackUpProcessResult.BackUpTime,
                Message = "Back up profile successfully" };

            BackUpFormAction.Invoke(this, eventData);
            Close();
        }

        void BackUpProcessUpdateHandler(object sender, BackUpProcessEventArgs e)
        {
            backupProcessingUserControl.backup_ProgressPanel.Text = e.Message;
            backupProcessingUserControl.lbl_backupProcessing_status.Text = e.Message;
            backupProcessingUserControl.backup_ProgressBar.Position = e.Percent;
        }

            #endregion

        #region Background processes
        void work_sizesItems_DoWork(object sender, DoWorkEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            BackupProcess.GetSizeOfFiles(BackupDataItems, work_sizesItems);
        }

        void work_sizesItems_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backupSettingsUserControl.gridControl1.DataSource = BackupDataItems;
            backupSettingsUserControl.gridControl_BackupItems.RefreshData();
        }

        void work_UpdateBackupStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            List<BackupDataItem> listItems = GetSelectedItems().Concat(CommonBackupDataItems).ToList();
            BackUpResult result = BackupProcess.StartBackUp(backupSettingsUserControl.txt_BackupName.Text, listItems);
            BackUpProcessResult = result;
        }

        void work_BackUp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => UpdateUIForStep(BackupStep.BACKUP_COMPLETE);
    #endregion
    }
}
