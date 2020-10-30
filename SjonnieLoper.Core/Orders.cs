using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SjonnieLoper.Core
{
    public class Orders
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public ApplicationUser Customer { get; set; }

        [Required]
        [DisplayName("Orderd Wiskey")]
        public WhiskeyBase Orderd_Wiskey { get; set; }

        [Required]
        [DisplayName("Amount Orderd")]
        public int AmountOrderd { get; set; }

        [Required]
        public decimal Ordercost { get; set; }

        [Required]
        public bool Delivery { get; set; }
    }
}
