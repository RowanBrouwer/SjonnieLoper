using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public class SQLOrders : IOrders
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneral _generalContext;

        public SQLOrders(ApplicationDbContext db, IGeneral general)
        {
            this.db = db;
            _generalContext = general;
        }

        public async Task<Order> AddOrder(Order NewOrder)
        {
            await db.AddAsync(NewOrder);
            return NewOrder;
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
            return await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public Order UpdateOrder(Order updatedOrder)
        {
            var entity = db.Orders.Attach(updatedOrder);
            entity.State = EntityState.Modified;
            return updatedOrder;
        }
    }
}
