using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SjonnieLoper.Core
{
    public class WhiskeyBase
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBottling { get; set; }

        [Required]
        public WhiskeyType Type { get; set; }

        [Required]
        [DisplayName("Country of origin")]
        public string CountryOforigin { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public double Procentage { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public int AmountInStorage { get; set; }

        [Required]
        public bool SoftDeleted { get; set; }  
    }
}
