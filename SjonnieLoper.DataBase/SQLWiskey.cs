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
            throw new NotImplementedException();
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
                
            }
            return DelOrder;
        }

        public ApplicationUser DeleteUser(string name)
        {
            var appuser = GetUserByName(name);
            if (appuser != null)
            {
                appuser.Deleted = true;
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

        public OrdersAndReservations GetAllOrdersAndReservations(string name)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int GetCountOfSpecificOrders(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
