using System;
using System.Linq;

namespace ZChangerMMO.Events
{
    public enum BackUpFormActionType
    {
        FINISH_BACKUP,
        CLOSE_DIALOG
    }

    public class BackUpFormEventArgs : EventArgs
    {
        public BackUpFormEventArgs() { }

        public BackUpFormActionType ActionType { get; set; }

        public string BackupName { get; set; }

        public DateTime BackupTime { get; set; }

        public string FilePath { get; set; }

        public string Message { get; set; }
    }
}
