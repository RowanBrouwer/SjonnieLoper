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
    public class DeleteModel : PageModel
    {
        private readonly IWiskey context;

        public OrdersAndReservations Order { get; set; }


        public DeleteModel(IWiskey context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int OrderId)
        {
            Order = context.GetOrderById(OrderId);
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int OrderId)
        {
            Order = context.DeleteOrder(OrderId);
            context.Commit();
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{Order.Id}{Order.Orderd_Wiskey.Brand}{Order.Orderd_Wiskey.Name} order deleted";
            return RedirectToPage("./Index");
        }
    }
}
