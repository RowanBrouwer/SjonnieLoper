using System;
using System.Collections.Generic;
using System.Text;
using SjonnieLoper.Core;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase
{
    public interface IWiskey
    {
        //whiskey commands//

        public Task<IEnumerable<WhiskeyBase>> GetAllWhiskeys(string name);
        public Task<IEnumerable<WhiskeyBase>> GetAllWhiskeysSearch(string searchName, string searchBrand, string searchCountry, 
            bool searchForType, WhiskeyType searchType,
            bool searchRangeAge, int searchAge1, int searchAge2,
            bool searchRangePrice, decimal searchPrice1, decimal searchPrice2,
            bool searchRangePercent, decimal searchPercent1, decimal searchPercent2);

        public Task<WhiskeyBase> GetWhiskeyById(int id);
        public WhiskeyBase UpdateWiskey(WhiskeyBase UpdatedWhiskey);
        public Task<WhiskeyBase> AddWhiskey(WhiskeyBase NewWhiskey);
        public Task<WhiskeyBase> DeleteWhiskey(int id);
        public Task<int> GetCountOfWhiskeys();

        //user commands//

        public Task<ApplicationUser> GetUserByNameAsync(string name);
        public ApplicationUser UpdateUserInfo(ApplicationUser updatedUser);
        public Task<ApplicationUser> AddUser(ApplicationUser NewUser);
        public Task<ApplicationUser> DeleteUser(string name);

        //Order commands//

        public Task<IEnumerable<OrdersAndReservations>> GetAllOrdersAndReservations(string name);
        public Task<OrdersAndReservations> GetOrderById(int id);
        public OrdersAndReservations UpdateOrder(OrdersAndReservations updatedOrder);
        public Task<OrdersAndReservations> AddOrder(OrdersAndReservations NewOrder);
        public Task<OrdersAndReservations> DeleteOrder(int id);
        public Task<int> GetCountOfOrders();

        //general commands//

        public Task<int> Commit();
        
    }
}
