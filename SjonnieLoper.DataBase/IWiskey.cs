using System;
using System.Collections.Generic;
using System.Text;
using SjonnieLoper.Core;
using System.Collections;

namespace SjonnieLoper.DataBase
{
    public interface IWiskey
    {
        //whiskey commands//

        IEnumerable<WhiskeyBase> GetAllWhiskeys(string name);
        WhiskeyBase GetWhiskeyById(int id);
        WhiskeyBase UpdateWiskey(WhiskeyBase UpdatedWhiskey);
        WhiskeyBase AddWhiskey(WhiskeyBase NewWhiskey);
        WhiskeyBase DeleteWhiskey(int id);
        int GetCountOfWhiskeys();
        int GetCountOfSpecificWhiskey(int id);

        //user commands//

        ApplicationUser GetUserByName(string name);
        ApplicationUser UpdateUserInfo(ApplicationUser updatedUser);
        ApplicationUser AddUser(ApplicationUser NewUser);
        ApplicationUser DeleteUser(string name);

        //Order commands//

        IEnumerable<OrdersAndReservations> GetAllOrdersAndReservations(string name);
        OrdersAndReservations GetOrderById(int id);
        OrdersAndReservations UpdateOrder(OrdersAndReservations updatedOrder);
        OrdersAndReservations AddOrder(OrdersAndReservations NewOrder);
        OrdersAndReservations DeleteOrder(int id);
        int GetCountOfOrders();

        //general commands//

        int Commit();
        
    }
}
