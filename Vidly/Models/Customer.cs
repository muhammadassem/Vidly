using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sorry Name Is Required")]
        [MaxLength(255)]
        [Column("Name", TypeName = "varchar")]
        public string Name { get; set; }

        [Display(Name = "Subscribe to Newsletter")]
        public bool IsSubscribedToNewsletter { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }
        

        [Display(Name = "Date of Birth")]
        [CustomerMustbe18YearsOld]
        public DateTime? Birthdate { get; set; }
    }
}