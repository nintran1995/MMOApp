using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ZChangerMMO.Models
{
    public enum DeviceType
    {
        WindowsPC,
        Macintosh,
        iPhone,
        iPad,
        Samsung,
        Xiaomi,
        Oppo
    }

    public class Device
    {
        [Key, Display(AutoGenerateField = false)]
        public long ID { get; set; }

        [Required, StringLength(30, MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [EnumDataType(typeof(DeviceType))]
        [Display(Name = "Type")]
        public DeviceType Type { get; set; }

        [Display(AutoGenerateField = false)]
        public bool Running { get; set; }

        [Display(AutoGenerateField = false)]
        public long EmailID { get; set; }

        [Display(AutoGenerateField = false)]
        public virtual Email Email { get; set; }
    }
}
