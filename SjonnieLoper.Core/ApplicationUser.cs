﻿using Microsoft.AspNetCore.Identity;
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

        public bool Deleted { get; set; }

    }
}
