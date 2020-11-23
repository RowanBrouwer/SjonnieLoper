using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SjonnieLoper.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int WhiskeyId { get; set; }
        public int Amount { get; set; }
        public WhiskeyBase Whiskey { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
    }
}
