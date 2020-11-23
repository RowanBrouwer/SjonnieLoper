using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        #region properties
        [TempData]
        public string Message { get; set; }
        public IEnumerable<Order> Orders{ get; set; }



        #region Search properties
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SearchAge1 { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SearchAge2 { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchRangeAge { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IncludeSoftDelete { get; set; }
        #endregion
        #endregion

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
            Orders = await _orderContext.GetAllOrdersAsync(
                SearchName,
                SearchRangeAge, SearchAge1, SearchAge2,
                IncludeSoftDelete);
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
