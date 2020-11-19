using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SjonnieLoper.Core.Models;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public class SQLOrders : IOrders
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneral _generalContext;
        private readonly ShoppingCart _shoppingCart;

        public SQLOrders(ApplicationDbContext db, IGeneral general, ShoppingCart shoppingCart)
        {
            this.db = db;
            _generalContext = general;
            _shoppingCart = shoppingCart;
        }

        /// <summary>
        /// Old method Rowan. Replaced by CreateOrderAsync
        /// </summary>
        /// <param name="NewOrder"></param>
        /// <returns></returns>
        /*public async Task<Order> AddOrder(Order NewOrder)
        {
            await db.AddAsync(NewOrder);
            return NewOrder;
        }*/

        public async Task CreateOrderAsync(ApplicationUser user)
        {
            Order newOrder = new Order
            {
                Customer = user
            };

            db.Add(newOrder);
            await _generalContext.Commit();

            //Per ShoppingCart item, make new OrderItem.
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            foreach (var cartItem in shoppingCartItems)
            {
                var orderItem = new OrderItem()
                {
                    Amount = cartItem.Amount,
                    WhiskeyId = cartItem.Whiskey.Id,
                    OrderId = newOrder.Id,
                    SubTotal = cartItem.SubTotal
                };
                db.OrderItems.Add(orderItem);

                //For each orderItem add Amount and Subtotal to TotalBottleAmount and TotalCost.
                newOrder.TotalBottleAmount += orderItem.Amount;
                newOrder.TotalCost += orderItem.SubTotal;
            }

            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Soft deletes an Order by Id.
        /// </summary>
        public async Task<Order> DeleteOrder(int id)
        {
            var DelOrder = await GetOrderById(id);
            if (DelOrder != null)
            {
                DelOrder.SoftDeleted = true;
            }
            return DelOrder;
        }

        /// <summary>
        /// Rowans method. Including search.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetAllOrdersAndReservations(string name)
        {
            var query = from o in db.Orders
                        where o.SoftDeleted == false
                        select o;
            return await query.ToListAsync();
        }

        /// <summary>
        /// Stella's method. Excluding search.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var query = from o in db.Orders
                        .Include(o => o.Customer)
                        .Include(o => o.OrderItems).ThenInclude(oi => oi.Whiskey)
                        where o.SoftDeleted == false
                        select o;
            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets count of all orders, deleted orders included.
        /// </summary>
        public async Task<int> GetCountOfOrders()
        {
            return await db.Orders.CountAsync();
        }

        /// <summary>
        /// Gets Order based on Id.
        /// </summary>
        public async Task<Order> GetOrderById(int id)
        {
            return await db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Whiskey)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Order UpdateOrder(Order updatedOrder)
        {
            var entity = db.Orders.Attach(updatedOrder);
            entity.State = EntityState.Modified;
            return updatedOrder;
        }
    }
}
