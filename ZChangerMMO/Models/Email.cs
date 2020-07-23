using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZChangerMMO.Models
{
    public class Email
    {
        [Key, Display(AutoGenerateField = false)]
        public long ID { get; set; }

        [Required, StringLength(30, MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string EmailAccount { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
