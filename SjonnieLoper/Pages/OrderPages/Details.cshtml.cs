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

namespace SjonnieLoper.Pages.OrderPages
{
    public class DetailsModel : PageModel
    {
        private readonly IWiskey context;

        public OrdersAndReservations Order { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(IWiskey context)
        {
            this.context = context;
        }

        public OrdersAndReservations OrdersAndReservations { get; set; }

        public async Task<IActionResult> OnGetAsync(int OrderId)
        {
            Order = await context.GetOrderById(OrderId);
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
