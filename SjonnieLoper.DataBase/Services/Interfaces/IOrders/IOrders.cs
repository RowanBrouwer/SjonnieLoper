using SjonnieLoper.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public interface IOrders
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync(
            string searchName,
            bool searchRangeAge, int searchAge1, int searchAge2,
            bool includeSoftDelete);
        public Task<Order> GetOrderById(int id);
        public Order UpdateOrder(Order updatedOrder);

        public Task CreateOrderAsync(ApplicationUser user);
        public Task<Order> DeleteOrderAsync(Order order);
        public Task<int> GetCountOfOrders();
    }
}
