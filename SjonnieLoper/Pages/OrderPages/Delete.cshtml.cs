using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Employee")]
    public class DeleteModel : PageModel
    {
        private readonly IWiskey _whiskeyContext;
        private readonly IGeneral _generalContext;
        private readonly IOrders _orderContext;

        public Order Order { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public DeleteModel(IWiskey context, IGeneral general, IOrders orderContext)
        {
            _whiskeyContext = context;
            _generalContext = general;
            _orderContext = orderContext;
        }

        public async Task<IActionResult> OnGet(int orderId)
        {
            Order = await _orderContext.GetOrderById(orderId);
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int orderId)
        {
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            Order = await _orderContext.DeleteOrderAsync(orderId);
            await _generalContext.Commit();

            TempData["Message"] = $"{Order.Id} order deleted";
            return RedirectToPage("./Index");
        }
    }
}
