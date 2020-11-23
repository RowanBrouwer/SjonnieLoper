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
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.OrderPages
{
    public class EditModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IGeneral general;
        private readonly IOrdersAndReservations orderContext;

        [BindProperty]
        public OrdersAndReservations OrdersAndReservations { get; set; }

        public EditModel(IWiskey context, IGeneral general, IOrdersAndReservations orderContext)
        {
            this.context = context;
            this.general = general;
            this.orderContext = orderContext;
        }


        public async Task<IActionResult> OnGet(int? OrderId)
        {
            if (OrderId.HasValue)
            {
                OrdersAndReservations = await orderContext.GetOrderById(OrderId.Value);
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
                orderContext.UpdateOrder(OrdersAndReservations);
            }
            else
            {
                orderContext.AddOrder(OrdersAndReservations);
            }
            general.Commit();
            TempData["Message"] = "Order saved!";
            return RedirectToPage("./Details", new { whiskeyId = OrdersAndReservations.Id });

        }

    }
}
