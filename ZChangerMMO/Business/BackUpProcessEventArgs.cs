using System;
using System.Linq;

namespace ZChangerMMO.BackupAndRestore
{
    public class BackUpProcessEventArgs : EventArgs
    {
        public BackUpProcessEventArgs() { }

        public bool IsFinish { get; set; }

        public string Message { get; set; }

        public int Percent { get; set; }
    }
}
