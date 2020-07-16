using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ZChangerMMO.DataModels
{
    public class ActionLog
    {
        public string ActionType { get; set; }

        public string Description { get; set; }

        [Key]
        [Display(AutoGenerateField = false)]
        public long Id { get; set; }

        public Profile profile { get; set; }

        public DateTime Time { get; set; }
    }
}
