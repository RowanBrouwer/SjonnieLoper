using System;
using System.Collections.Generic;
using System.Text;

namespace SjonnieLoper.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public WhiskeyBase Whiskey { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
