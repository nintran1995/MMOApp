using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ZChangerMMO.DataModels
{
    public class BackupItem
    {
        public DateTime BackupTime { get; set; }

        public string FilePath { get; set; }

        [Key]
        [Display(AutoGenerateField = false)]
        public long Id { get; set; }

        public string Name { get; set; }

        public Profile profile { get; set; }
    }
}
