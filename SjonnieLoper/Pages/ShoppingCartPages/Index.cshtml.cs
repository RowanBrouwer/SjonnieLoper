using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services;

namespace SjonnieLoper.Pages.ShoppingCartPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey _whiskeyRepository;
        //private readonly ShoppingCart _shoppingCart;

        #region public properties
        public ShoppingCart _shoppingCart { get; set; }
        public decimal PriceTotal { get; set; }
        #endregion

        public IndexModel(IWiskey whiskeyRepository, ShoppingCart shoppingCart)
        {
            _whiskeyRepository = whiskeyRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> OnGet()
        {
            var items = await _shoppingCart.GetCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;


            PriceTotal = await _shoppingCart.GetCartTotalAsync();

            return Page();
        }

        public async Task<IActionResult> AddToShoppingCartAsync(int whiskeyId, int? amount)
        {
            int sAmount = 1;
            if (amount.HasValue)
                sAmount = (int)amount;

            var selectedWhiskey = await _whiskeyRepository.GetWhiskeyById(whiskeyId);

            if (selectedWhiskey != null)
            {
                await _shoppingCart.AddToCartAsync(selectedWhiskey, sAmount);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromShoppingCartAsync(int whiskeyId, int? amount)
        {
            int sAmount = 1;
            if (amount.HasValue)
                sAmount = (int)amount;

            var selectedWhiskey = await _whiskeyRepository.GetWhiskeyById(whiskeyId);

            if (selectedWhiskey != null)
            {
                await _shoppingCart.RemoveFromCartAsync(selectedWhiskey, sAmount);
            }
            return RedirectToAction("Index");
        }
    }
}
