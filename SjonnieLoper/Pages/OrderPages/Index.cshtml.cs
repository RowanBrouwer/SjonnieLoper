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
using SjonnieLoper.DataBase.Services;
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.OrderPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey _whiskeyRepository;
        private readonly IGeneral _generalContext;
        private readonly IOrders _orderContext;
        private readonly IAppUser _appUserRepository;
        private readonly ShoppingCart _shoppingCart;

        [TempData]
        public string Message { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string SearchTerm { get; set; }

        public IEnumerable<Order> Orders{ get; set; }

        public IndexModel(IWiskey context, IGeneral general, IOrders orderContext, IAppUser appUserContext, ShoppingCart shoppingCart)
        {
            _whiskeyRepository = context;
            _generalContext = general;
            _orderContext = orderContext;
            _appUserRepository = appUserContext;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderContext.GetAllOrdersAsync();
            return Page();
        }
        

        public async Task<IActionResult> OnPostAddOrderAsync()
        {
            var items = await _shoppingCart.GetCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                Message = "Your cart is empty, add some whiskey first.";
                return RedirectToPage("/ShoppingCartPages/Index");
            }

            var customer = await _appUserRepository.GetUserByNameAsync(User.Identity.Name);
            if (customer == null)
            {
                return RedirectToPage("/ShoppingCartPages/Index");
            }

            await _orderContext.CreateOrderAsync(customer);
            await _shoppingCart.ClearCartAsync();

            return RedirectToPage("Index");
        }
    }
}
