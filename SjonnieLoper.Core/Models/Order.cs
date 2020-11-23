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
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser Customer { get; set; }

        public IList<OrderItem> OrderItems { get; set; }

        public int TotalBottleAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }

        public bool SoftDeleted { get; set; }

    }
}
