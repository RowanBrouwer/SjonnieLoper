using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SjonnieLoper.Core
{
    public class WhiskeyBase
    {
        [Key]
        [Required]
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string Brand { get; set; }

        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBottling { get; set; }

        
        public WhiskeyType Type { get; set; }

        
        [DisplayName("Country of origin")]
        public string CountryOforigin { get; set; }

        
        public double Price { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:##}")]
        public double Procentage { get; set; }

        
        public string ImagePath { get; set; }

        
        public int AmountInStorage { get; set; }

        
        public bool SoftDeleted { get; set; }  
    }
}
