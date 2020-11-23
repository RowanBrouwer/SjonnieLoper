using SjonnieLoper.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public interface IOrdersAndReservations
    {
        public Task<IEnumerable<OrdersAndReservations>> GetAllOrdersAndReservations(string name);
        public Task<OrdersAndReservations> GetOrderById(int id);
        public OrdersAndReservations UpdateOrder(OrdersAndReservations updatedOrder);
        public Task<OrdersAndReservations> AddOrder(OrdersAndReservations NewOrder);
        public Task<OrdersAndReservations> DeleteOrder(int id);
        public Task<int> GetCountOfOrders();
    }
}
