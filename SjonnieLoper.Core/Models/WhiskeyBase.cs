using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.Core
{
    public class WhiskeyBase
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }


        [DisplayName("Age")]
        public int AgeYears { get; set; }

        public WhiskeyType Type { get; set; }
        

        [DisplayName("Country of origin")]
        public string CountryOfOrigin { get; set; }


        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Percentage { get; set; }
 
        public string ImagePath { get; set; }
        
        public int AmountInStorage { get; set; }
        
        public bool SoftDeleted { get; set; }  
    }
}
