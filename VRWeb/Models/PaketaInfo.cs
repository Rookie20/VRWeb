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

        [Display(Name = "Regular")]
        public bool regularPaketa { get; set; }

        [Display(Name = "Premium")]
        public bool premiumPaketa { get; set; }

        [Display(Name = "Shteti")]
        public string emriShtetit { get; set; }

        [Required]
        [Display(Name = "Qyteti")]
        [RegularExpression(@"^([a-zA-Z]{1}[a-zA-Z]*[\s]{0,1}[a-zA-Z])+([\s]{0,1}[a-zA-Z]+)", ErrorMessage = "Pick a valid City")]
        public string emriQytetit { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Numbers only")]
        [Range(1, 10, ErrorMessage = "You can only pick between 1 and 10 days of vacation")]
        [Display(Name = "Numri i diteve")]
        public string numriDiteve { get; set; }

        [Display(Name = "Yjet e Hotelit")]
        public int vendqendrimiHotelYje { get; set; }

        [Display(Name = "Çmimi")]
        public decimal cmimi { get; set; }

        [ForeignKey("ApplicationUser")]
        public string userKey { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}