using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public class SQLOrdersAndReservations : IOrdersAndReservations
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneral general;

        public SQLOrdersAndReservations(ApplicationDbContext db, IGeneral general)
        {
            this.db = db;
            this.general = general;
        }

        public async Task<OrdersAndReservations> AddOrder(OrdersAndReservations NewOrder)
        {
            await db.AddAsync(NewOrder);
            return NewOrder;
        }

        public async Task<OrdersAndReservations> DeleteOrder(int id)
        {
            var DelOrder = await GetOrderById(id);
            if (DelOrder != null)
            {
                DelOrder.SoftDeleted = true;
            }
            return DelOrder;
        }

        public async Task<IEnumerable<OrdersAndReservations>> GetAllOrdersAndReservations(string name)
        {
            var query = from o in db.Orders
                        where o.SoftDeleted == false
                        select o;
            return await query.ToListAsync();
        }

        public async Task<int> GetCountOfOrders()
        {
            return await db.Orders.CountAsync();
        }

        public async Task<OrdersAndReservations> GetOrderById(int id)
        {
            return await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public OrdersAndReservations UpdateOrder(OrdersAndReservations updatedOrder)
        {
            var entity = db.Orders.Attach(updatedOrder);
            entity.State = EntityState.Modified;
            return updatedOrder;
        }
    }
}
