using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sorry Name Is Required")]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        //[CustomerMustbe18YearsOld] // so we don't get errors as this validation class uses an Mvc model class not Dto class in side it (Customer)
        public DateTime? Birthdate { get; set; }
    }
}