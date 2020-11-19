using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.Core
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        //public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName 
        { 
            get 
            { 
                return FirstName + " " + LastName; 
            }
        }

        //Temporarily removed birthday. Easier to use int for Age.

        /* [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime Birthday  { get; set; }*/

        [Required]
        public int AgeYears { get; set; }

        public bool SoftDeleted { get; set; }

        public bool Employee { get; set; }
    }
}
