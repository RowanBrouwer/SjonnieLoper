using SjonnieLoper.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public interface IOrders
    {
        public Task<IEnumerable<Order>> GetAllOrdersAndReservations(string name);
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task<Order> GetOrderById(int id);
        public Order UpdateOrder(Order updatedOrder);
        public Task<Order> AddOrder(Order NewOrder);
        public Task<Order> DeleteOrder(int id);
        public Task<int> GetCountOfOrders();
    }
}
