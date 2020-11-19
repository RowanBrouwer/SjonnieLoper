using SjonnieLoper.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.Core
{
    public class OrdersAndReservations
    {
        [Key]
        public int Id { get; set; }
        
        public ApplicationUser Customer { get; set; }

        [DisplayName("Amount Orderd")]
        public int AmountOrderd { get; set; }

        public IList<ShoppingCartItem> CartItems { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Ordercost { get; set; }

        public bool SoftDeleted { get; set; }

    }
}
