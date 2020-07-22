using System;
using System.ComponentModel.DataAnnotations;

namespace ZChangerMMO.Models
{
    public enum DeviceType
    {
        Iphone,
        Samsung
    }

    public class Device
    {
        [Key, Display(AutoGenerateField = false)]
        public long ID { get; set; }

        [Display(AutoGenerateField = false)]
        public long EmailID { get; set; }

        [Display(AutoGenerateField = false)]
        public virtual Email Email { get; set; }

        [Required, StringLength(30, MinimumLength = 4)]
        [Display(Name = "NAME")]
        public string Name { get; set; }

        [EnumDataType(typeof(DeviceType))]
        [Display(Name = "DEVICE TYPE")]
        public DeviceType Type { get; set; }        
    }
}
