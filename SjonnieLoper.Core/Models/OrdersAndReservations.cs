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
        public int Id { get; set; }

        
        public ApplicationUser Customer { get; set; }

        
        [DisplayName("Orderd/Reservated Wiskey")]
        public WhiskeyBase Orderd_Wiskey { get; set; }

        
        [DisplayName("Amount Orderd")]
        public int AmountOrderd { get; set; }

        
        public double Ordercost { get; set; }


        public bool SoftDeleted { get; set; }

    }
}
