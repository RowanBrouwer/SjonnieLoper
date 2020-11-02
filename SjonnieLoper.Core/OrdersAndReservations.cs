using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SjonnieLoper.Core
{
    public class OrdersAndReservations
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public ApplicationUser Customer { get; set; }

        [Required]
        [DisplayName("Orderd/Reservated Wiskey")]
        public WhiskeyBase Orderd_Wiskey { get; set; }

        [Required]
        [DisplayName("Amount Orderd")]
        public int AmountOrderd { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:##.##}")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,20)")]
        public decimal Ordercost { get; set; }

    }
}
