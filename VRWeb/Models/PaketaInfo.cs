using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VRWeb.Models
{
    [Table("Paketa")]
    public class PaketaInfo
    {
        [Key]
        public int paketaID { get; set; }

        [Display(Name = "64 GB")]
        public bool regularPaketa { get; set; }

        [Display(Name = "256 GB")]
        public bool premiumPaketa { get; set; }

        [Display(Name = "Shteti")]
        [RegularExpression(@"^([a-zA-Z]{1}[a-zA-Z]*[\s]{0,1}[a-zA-Z])+([\s]{0,1}[a-zA-Z]+)", ErrorMessage = "Pick a valid Country")]
        public string emriShtetit { get; set; }

        [Required]
        [Display(Name = "Qyteti")]
        [RegularExpression(@"^([a-zA-Z]{1}[a-zA-Z]*[\s]{0,1}[a-zA-Z])+([\s]{0,1}[a-zA-Z]+)", ErrorMessage = "Pick a valid City")]
        public string emriQytetit { get; set; }

        [Required]
        [MaxLength(25)]
        public string zipCode { get; set; }

        [Display(Name = "Çmimi")]
        public decimal cmimi { get; set; }

        [ForeignKey("ApplicationUser")]
        public string userKey { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}