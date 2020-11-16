using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjonnieLoper.Core.Models;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services;
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.ShoppingCartPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey _whiskeyRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IGeneral general;

        #region public properties
        //public ShoppingCart _shoppingCart { get; set; }
        [BindProperty]
        public List<ShoppingCartItem> ShCartItems { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal PriceTotal { get; set; }
        #endregion

        public IndexModel(IWiskey whiskeyRepository, ShoppingCart shoppingCart, IGeneral general)
        {
            _whiskeyRepository = whiskeyRepository;
            _shoppingCart = shoppingCart;
            this.general = general;
        }

        public async Task<IActionResult> OnGet()
        {
            var items = await _shoppingCart.GetCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            ShCartItems = items;
            PriceTotal = await _shoppingCart.GetCartTotalAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddToShoppingCartAsync(int whiskeyId, int? amount)
        {
            int sAmount = 1;
            if (amount.HasValue)
                sAmount = (int)amount;

            var selectedWhiskey = await _whiskeyRepository.GetWhiskeyById(whiskeyId);

            if (selectedWhiskey != null)
            {
                await _shoppingCart.AddToCartAsync(selectedWhiskey, sAmount);
            }
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostRemoveFromShoppingCartAsync(int whiskeyId, int? amount)
        {
            int sAmount = 1;
            if (amount.HasValue)
                sAmount = (int)amount;

            var selectedWhiskey = await _whiskeyRepository.GetWhiskeyById(whiskeyId);

            if (selectedWhiskey != null)
            {
                await _shoppingCart.RemoveFromCartAsync(selectedWhiskey, sAmount);
            }
            return RedirectToPage("Index");
        }

        /* Work in progress.

        public async Task<IActionResult> OnPostRemoveItemFromShoppingCartAsync(int itemId)
        {
            var selectedItem = await _whiskeyRepository.GetCartItemByIdAsync(itemId);

            if (selectedItem != null)
            {
                await _shoppingCart.RemoveItemFromCartAsync(selectedItem);
            }
            return RedirectToPage("Index");
        }
        */
    }
}
