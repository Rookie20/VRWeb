using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VRWeb.Models
{
    [Table("Contact")]
    public class ContactModels
    {
        [Key]
        public int ContactID { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        [Required]
        public string Message { get; set; }
    }
}