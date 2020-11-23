using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.OrderPages
{
    public class DetailsModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IGeneral general;
        private readonly IOrdersAndReservations orderContext;

        public OrdersAndReservations Order { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(IWiskey context, IGeneral general, IOrdersAndReservations orderContext)
        {
            this.context = context;
            this.general = general;
            this.orderContext = orderContext;
        }

        public OrdersAndReservations OrdersAndReservations { get; set; }

        public async Task<IActionResult> OnGetAsync(int OrderId)
        {
            Order = await orderContext.GetOrderById(OrderId);
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
