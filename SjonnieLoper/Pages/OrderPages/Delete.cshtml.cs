using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Models;
using SjonnieLoper.Data;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.OrderPages
{
    public class DeleteModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IGeneral general;
        private readonly IOrdersAndReservations orderContext;

        public OrdersAndReservations Order { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public DeleteModel(IWiskey context, IGeneral general, IOrdersAndReservations orderContext)
        {
            this.context = context;
            this.general = general;
            this.orderContext = orderContext;
        }

        public async Task<IActionResult> OnGet(int OrderId)
        {
            Order = await orderContext.GetOrderById(OrderId);
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int OrderId)
        {
            Order = await orderContext.DeleteOrder(OrderId);
            await general.Commit();
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{Order.Id}{Order}{Order} order deleted";
            return RedirectToPage("./Index");
        }
    }
}
