using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using System.Threading.Tasks;
using SjonnieLoper.Core.Models;

namespace SjonnieLoper.DataBase
{
    public class SQLWiskey : IWiskey
    {
        private readonly ApplicationDbContext db;

        public SQLWiskey(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<OrdersAndReservations> AddOrder(OrdersAndReservations NewOrder)
        {
            await db.AddAsync(NewOrder);
            return NewOrder;
        }

        public async Task<ApplicationUser> AddUser(ApplicationUser NewUser)
        {
            await db.AddAsync(NewUser);
            return NewUser;
        }

        public async Task<WhiskeyBase> AddWhiskey(WhiskeyBase NewWhiskey)
        {
            await db.AddAsync(NewWhiskey);
            return NewWhiskey;
        }

        public async Task<int> Commit()
        {
            int PB = await db.SaveChangesAsync();
            return PB;
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

        public async Task<ApplicationUser> DeleteUser(string name)
        {
            var appuser = await GetUserByNameAsync(name);
            if (appuser != null)
            {
                appuser.SoftDeleted = true;
            }
            return appuser;
        }

        public async Task<WhiskeyBase> DeleteWhiskey(int id)
        {
            var whiskey = await GetWhiskeyById(id);
            if (whiskey != null)
            {
                whiskey.SoftDeleted = true;
            }
            return whiskey;
        }

        public async Task<IEnumerable<OrdersAndReservations>> GetAllOrdersAndReservations(string name)
        {
            var query = from o in db.Orders
                        where o.SoftDeleted == false
                        where o.Customer.FName.StartsWith(name) ||
                        o.Customer.LName.StartsWith(name) ||
                        o.Customer.Email.StartsWith(name) ||
                        o.Orderd_Wiskey.Name.StartsWith(name) ||
                        o.Orderd_Wiskey.Brand.StartsWith(name) ||
                        o.Orderd_Wiskey.CountryOfOrigin.Country.Contains(name)
                        orderby o.AmountOrderd
                        select o;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<WhiskeyBase>> GetAllWhiskeys(string name)
        {

            var query = db.Whiskeys.Where(w => w.SoftDeleted == false && w.Name.Contains(name) || string.IsNullOrEmpty(name) || w.Brand.Contains(name) || w.CountryOfOrigin.Country.Contains(name)).OrderBy(w => w.Name);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<WhiskeyBase>> GetAllWhiskeysSearch(string searchName, string searchBrand, string searchCountry, bool searchForType, WhiskeyType searchType)
        {
            var query = from w in db.Whiskeys
                        where w.SoftDeleted == false
                        where (string.IsNullOrEmpty(searchName) || w.Name.Contains(searchName))
                        where (string.IsNullOrEmpty(searchBrand) || w.Brand.Contains(searchBrand))
                        where (string.IsNullOrEmpty(searchCountry) || w.CountryOfOrigin.Country.Contains(searchCountry))
                        where (!searchForType || w.Type == searchType)
                        orderby w.Name
                        select w;

            return await query.ToListAsync();
        }

        public async Task<int> GetCountOfOrders()
        {
            return await db.Orders.CountAsync();
        }

        public async Task<int> GetCountOfWhiskeys()
        {
            return await db.Whiskeys.CountAsync();
        }

        public async Task<OrdersAndReservations> GetOrderById(int id)
        {
            return await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string name)
        {
            return await db.ApplicationUsers.FirstOrDefaultAsync(a => a.UserName == name);
        }

        public async Task<WhiskeyBase> GetWhiskeyById(int id)
        {
            return await db.Whiskeys.Include(w => w.CountryOfOrigin).FirstOrDefaultAsync(w => w.Id == id);
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
