using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Models;
using SjonnieLoper.Data;

namespace SjonnieLoper.DataBase.Services
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _appDbContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        /// <summary>
        /// Checks if this session already has a ShoppingCart. If so returns that ShoppingCart, if not creates a new ShoppingCart for it.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();

            string cartID = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();

            session.SetString("ShoppingCartId", cartID);

            return new ShoppingCart(context) { ShoppingCartId = cartID };
        }

        /// <summary>
        /// Sets the ShoppingCart to the same instance each time. This is for testing purposes.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ShoppingCart GetCartDebug(IServiceProvider services)
        {
            string cartID = "1";

            var context = services.GetService<ApplicationDbContext>();

            return new ShoppingCart(context) { ShoppingCartId = cartID };
        }

        /// <summary>
        /// Creates a new ShoppingCartItem for selected whiskey or adds more if SHCartItem already existed.
        /// </summary>
        /// <param name="whiskey">Whiskey that is being added.</param>
        /// <param name="amount">Amount of whiskey added to the cart.</param>
        public async Task AddToCartAsync(WhiskeyBase whiskey, int amount)
        {
            //Check if that whiskey is already in cart.
            var ShCartItem = await _appDbContext.ShoppingCartItems.SingleOrDefaultAsync(
                s => s.Whiskey.Id == whiskey.Id && s.ShoppingCartId == ShoppingCartId);

            //If whiskey is not already in cart, make new CartItem with specified amount.
            //Otherwise add new amount to already added amount.
            if (ShCartItem == null)
            {
                ShCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Whiskey = whiskey,
                    Amount = amount
                };
                _appDbContext.ShoppingCartItems.Add(ShCartItem);
            }
            else
                ShCartItem.Amount += amount;  

            await _appDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a specific ShoppingCartItem entirely from the Cart.
        /// </summary>
        /// <param name="cItem">ShoppingCartItem</param>
        public async Task RemoveItemFromCartAsync(ShoppingCartItem cItem)
        {
            ShoppingCartItem ShCartItem = await _appDbContext.ShoppingCartItems.SingleOrDefaultAsync(
                s => s.Id == cItem.Id && s.ShoppingCartId == cItem.ShoppingCartId);

            if (ShCartItem != null)
            {
                _appDbContext.ShoppingCartItems.Remove(ShCartItem);
            }

            await _appDbContext.SaveChangesAsync();
        }
        

        /// <summary>
        /// Removes a certain amount of whiskey from cart.
        /// </summary>
        /// <param name="whiskey">Specified whiskey to remove.</param>
        /// <param name="amount">Amount to remove.</param>
        public async Task RemoveFromCartAsync(WhiskeyBase whiskey, int amount)
        {
            //Check for whiskey in the cart.
            var ShCartItem = await _appDbContext.ShoppingCartItems.SingleOrDefaultAsync(
               s => s.Whiskey.Id == whiskey.Id && s.ShoppingCartId == ShoppingCartId);

            if (ShCartItem != null)
            {
                //If removed amount is less than amount in cart, adjust amount.
                //Otherwise remove ShCartItem entirely.
                if ((ShCartItem.Amount - amount) >= 1)
                {
                    ShCartItem.Amount -= amount;
                }
                else
                    _appDbContext.ShoppingCartItems.Remove(ShCartItem);
            }

            await _appDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the ShoppingCartItems were already retrieved. If not, retrieves them.
        /// </summary>
        /// <returns>CurrentSession ShoppingCartItems</returns>
        public async Task<List<ShoppingCartItem>> GetCartItemsAsync()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = await _appDbContext.ShoppingCartItems
                   .Where(c => c.ShoppingCartId == ShoppingCartId)
                   .Include(s => s.Whiskey)
                   .ToListAsync());
        }

        /// <summary>
        /// Removes all items from Cart.
        /// </summary>
        public async Task ClearCartAsync()
        {
            var ShCartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(ShCartItems);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAsync()
        {
            var total = await _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Whiskey.Price * c.Amount).SumAsync();

            return total;
        }

    }
}
