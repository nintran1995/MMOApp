using System;
using System.Linq;

namespace ZChangerMMO.BackupAndRestore
{
    public enum BackupDataItemType
    {
        FILE,
        FOLDER
    }

    public enum ItemLevel
    {
        USER,
        PROFILE
    }

    public class BackupDataItem
    {
        public ItemLevel ItemLevel { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public BackupDataItemType Type { get; set; }
    }
}
