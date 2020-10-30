using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SjonnieLoper.Core
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FName { get; set; }

        public string MName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Birthday  { get; set; }

        public int ZipCodeNum { get; set; }

        public string ZipCodeLet { get; set; }

        public int HouseNumber { get; set; }

        public string HouseNumberAddon { get; set; }

        public string Country { get; set; }

        public string StreetName { get; set; }

        public bool Employee { get; set; }

        public bool Deleted { get; set; }

    }
}
