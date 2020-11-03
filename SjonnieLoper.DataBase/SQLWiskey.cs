using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SjonnieLoper.Core;
using SjonnieLoper.Data;

namespace SjonnieLoper.DataBase
{
    public class SQLWiskey : IWiskey
    {
        private readonly ApplicationDbContext db;

        public SQLWiskey(ApplicationDbContext db)
        {
            this.db = db;
        }

        public OrdersAndReservations AddOrder(OrdersAndReservations NewOrder)
        {
            db.Add(NewOrder);
            return NewOrder;
        }

        public ApplicationUser AddUser(ApplicationUser NewUser)
        {
            db.Add(NewUser);
            return NewUser;
        }

        public WhiskeyBase AddWhiskey(WhiskeyBase NewWhiskey)
        {
            db.Add(NewWhiskey);
            return NewWhiskey;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public OrdersAndReservations DeleteOrder(int id)
        {
            var DelOrder = GetOrderById(id);
            if (DelOrder != null)
            {
                DelOrder.SoftDeleted = true;
            }
            return DelOrder;
        }

        public ApplicationUser DeleteUser(string name)
        {
            var appuser = GetUserByName(name);
            if (appuser != null)
            {
                appuser.SoftDeleted = true;
            }
            return appuser;
        }

        public WhiskeyBase DeleteWhiskey(int id)
        {
            var whiskey = GetWhiskeyById(id);
            if (whiskey != null)
            {
                whiskey.SoftDeleted = true;
            }
            return whiskey;
        }

        public IEnumerable<OrdersAndReservations> GetAllOrdersAndReservations(string name)
        {
            var query = from o in db.Orders
                        where o.SoftDeleted == false
                        where o.Customer.FName.StartsWith(name) ||
                        o.Customer.LName.StartsWith(name) ||
                        o.Customer.Email.StartsWith(name) ||
                        o.Orderd_Wiskey.Name.StartsWith(name) ||
                        o.Orderd_Wiskey.Brand.StartsWith(name) ||
                        o.Orderd_Wiskey.CountryOforigin.StartsWith(name)
                        orderby o.AmountOrderd
                        select o;
            return query;
        }

        public IEnumerable<WhiskeyBase> GetAllWhiskeys(string name)
        {
            var query = from w in db.Whiskeys
                        where w.SoftDeleted == false
                        where w.Name.StartsWith(name) || string.IsNullOrEmpty(name) || w.Brand.StartsWith(name) || w.CountryOforigin.StartsWith(name)
                        orderby w.Name
                        select w;
            return query;
        }

        public int GetCountOfOrders()
        {
            return db.Orders.Count();
        }

        public int GetCountOfSpecificWhiskey(int id)
        {
            return GetWhiskeyById(id).AmountInStorage;
        }

        public int GetCountOfWhiskeys()
        {
            return db.Whiskeys.Count();
        }

        public OrdersAndReservations GetOrderById(int id)
        {
            return db.Orders.FirstOrDefault(o => o.Id == id);
        }

        public ApplicationUser GetUserByName(string name)
        {
            return db.ApplicationUsers.FirstOrDefault(a => a.UserName == name);
        }

        public WhiskeyBase GetWhiskeyById(int id)
        {
            return db.Whiskeys.FirstOrDefault(w => w.Id == id);
        }

        public OrdersAndReservations UpdateOrder(OrdersAndReservations updatedOrder)
        {
            var entity = db.Orders.Attach(updatedOrder);
            entity.State = EntityState.Modified;
            return updatedOrder;
        }

        public ApplicationUser UpdateUserInfo(ApplicationUser updatedUser)
        {
            var entity = db.ApplicationUsers.Attach(updatedUser);
            entity.State = EntityState.Modified;
            return updatedUser;
        }

        public WhiskeyBase UpdateWiskey(WhiskeyBase UpdatedWhiskey)
        {
            var entity = db.Whiskeys.Attach(UpdatedWhiskey);
            entity.State = EntityState.Modified;
            return UpdatedWhiskey;
        }
    }
}
