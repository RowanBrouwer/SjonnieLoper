using Microsoft.AspNetCore.Mvc;
using SjonnieLoper.DataBase.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Components
{
    public class ShoppingCartWidgetViewComponent : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartWidgetViewComponent(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _shoppingCart.GetCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            return View(items);
        }
    }
}
