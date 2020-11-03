using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using SjonnieLoper.DataBase;

namespace SjonnieLoper.Pages.OrderPages
{
    public class EditModel : PageModel
    {
        private readonly IWiskey context;

        [BindProperty]
        public OrdersAndReservations OrdersAndReservations { get; set; }

        public EditModel(IWiskey context)
        {
            this.context = context;
        }


        public IActionResult OnGet(int? OrderId)
        {
            if (OrderId.HasValue)
            {
                OrdersAndReservations = context.GetOrderById(OrderId.Value);
            }
            else
            {
                OrdersAndReservations = new OrdersAndReservations();
            }
            if (OrderId == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (OrdersAndReservations.Id > 0)
            {
                context.UpdateOrder(OrdersAndReservations);
            }
            else
            {
                context.AddOrder(OrdersAndReservations);
            }
            context.Commit();
            TempData["Message"] = "Order saved!";
            return RedirectToPage("./Details", new { whiskeyId = OrdersAndReservations.Id });

        }

    }
}
