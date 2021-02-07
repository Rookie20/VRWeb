using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRWeb.Models
{
    [Table("ContactUs")]
    public class ContactUsPage
    {
        [Key]
        public int contactID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters!")]
        [Display(Name = "Name")]
        [RegularExpression(@"^([a-zA-Z]{1}[a-zA-Z]*[\s]{0,1}[a-zA-Z])+([\s]{0,1}[a-zA-Z]+)", ErrorMessage = "Only letters and carefull with spaces!")]

        public string contactName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "No Special Characters")]
        public string contactEmail { get; set; }

        [Required]
        [Phone]
        [Display(Name ="Phone Number")]
        public string contactPhone { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 500 characters!")]
        [Display(Name = "Message")]
        public string contactMessage { get; set; }
    }
}