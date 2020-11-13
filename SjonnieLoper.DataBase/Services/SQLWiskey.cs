using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using System.Threading.Tasks;
using SjonnieLoper.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<WhiskeyBase> AddWhiskey(WhiskeyBase NewWhiskey, bool newCountry, string Country)
        {
            if (newCountry == true)
            {
                if (db.Countries.FirstOrDefault(c => c.Name == Country) == null)
                {
                    Country newCustomCountry = new Country
                    {
                        Name = Country
                    };
                    await Commit();

                    NewWhiskey.CountryOfOrigin = newCustomCountry;

                    await db.AddAsync(NewWhiskey);
                }
                else
                {
                    NewWhiskey.CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == Country);
                    await db.AddAsync(NewWhiskey);
                }
            }

            else if (newCountry == false && Country == null)
            {
                await db.AddAsync(NewWhiskey);
            }
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
                        o.Orderd_Wiskey.CountryOfOrigin.Name.Contains(name)
                        orderby o.AmountOrderd
                        select o;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<WhiskeyBase>> GetAllWhiskeys(string name)
        {

            var query = from w in db.Whiskeys
                        where w.SoftDeleted == false
                        where string.IsNullOrEmpty(name) || w.Name.Contains(name) || w.Brand.Contains(name) || w.CountryOfOrigin.Name.Contains(name)
                        orderby w.Name
                        select w;

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<WhiskeyBase>> GetAllWhiskeysSearch(string searchName, string searchBrand, string searchCountry, 
            bool searchForType, WhiskeyType searchType,
            bool searchRangeAge, int searchAge1, int searchAge2,
            bool searchRangePrice, decimal searchPrice1, decimal searchPrice2,
            bool searchRangePercent, decimal searchPercent1, decimal searchPercent2)
        {
            var query = from w in db.Whiskeys
                        where w.SoftDeleted == false
                        where (string.IsNullOrEmpty(searchName) || w.Name.Contains(searchName))
                        where (string.IsNullOrEmpty(searchBrand) || w.Brand.Contains(searchBrand))
                        where (string.IsNullOrEmpty(searchCountry) || w.CountryOfOrigin.Name.Contains(searchCountry))
                        where (!searchForType || w.Type == searchType)
                        where (!searchRangeAge && (searchAge1 == 0 || w.AgeYears == searchAge1) || w.AgeYears >= searchAge1 && w.AgeYears <= searchAge2)
                        where (!searchRangePrice && (searchPrice1 == 0 || w.Price == searchPrice1) || w.Price >= searchPrice1 && w.Price <= searchPrice2)
                        where (!searchRangePercent && (searchPercent1 == 0 || w.Percentage == searchPercent1) || w.Percentage >= searchPercent1 && w.Percentage <= searchPercent2)
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

        /// <summary>
        /// Rowans GetAllCountries
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetAllCountrys()
        {
            var countrys = from c in db.Countries
                        orderby c.Name
                        select c;
            return await countrys.ToListAsync();
        }

        /// <summary>
        /// Stella's GetAllCountries
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await db.Countries.ToListAsync();
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

        /// <summary>
        /// Rowans Update Code.
        /// </summary>
        /// <param name="UpdatedWhiskey"></param>
        /// <param name="newCountry"></param>
        /// <param name="Country"></param>
        /// <returns></returns>
        public WhiskeyBase UpdateWiskey(WhiskeyBase UpdatedWhiskey, bool newCountry, string Country)
        {
            if (newCountry == true)
            {
                if (db.Countries.FirstOrDefault(c => c.Name == Country) == null)
                {
                    Country newCustomCountry = new Country
                    {
                        Name = Country
                    };
                    db.Add(newCountry);

                    UpdatedWhiskey.CountryOfOrigin = newCustomCountry;

                    var entity = db.Whiskeys.Attach(UpdatedWhiskey);

                    entity.State = EntityState.Modified;
                }
                else
                {
                    UpdatedWhiskey.CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == Country);
                    var entity = db.Whiskeys.Attach(UpdatedWhiskey);

                    entity.State = EntityState.Modified;
                }
            }
            else if (newCountry == false && Country == null)
            {
                var entity = db.Whiskeys.Attach(UpdatedWhiskey);

                entity.State = EntityState.Modified;
            }
            return UpdatedWhiskey;
        }

        /// <summary>
        /// Stella's update code.
        /// </summary>
        /// <param name="UpdatedWhiskey"></param>
        /// <param name="addNewCountry">Bool to check if a new country is being added.</param>
        /// <param name="CountryName">Name of new country being added.</param>
        /// <returns></returns>
        public async Task<WhiskeyBase> UpdateWiskeyAsync(WhiskeyBase UpdatedWhiskey, bool addNewCountry, string CountryName)
        {
            if (addNewCountry)
            {
                if ((await db.Countries.FirstOrDefaultAsync(c => c.Name == CountryName)) == null)
                {
                    Country customCountry = new Country
                    {
                        Name = CountryName
                    };
                    db.Add(customCountry);

                    UpdatedWhiskey.CountryOfOrigin = customCountry;

                    var entity = db.Whiskeys.Attach(UpdatedWhiskey);

                    entity.State = EntityState.Modified;
                }
                else
                {
                    UpdatedWhiskey.CountryOfOrigin = await db.Countries.FirstOrDefaultAsync(c => c.Name == CountryName);
                    var entity = db.Whiskeys.Attach(UpdatedWhiskey);

                    entity.State = EntityState.Modified;
                }
            }
            else if (!addNewCountry && CountryName == null)
            {
                var entity = db.Whiskeys.Attach(UpdatedWhiskey);

                entity.State = EntityState.Modified;
            }
            return UpdatedWhiskey;
        }
    }
}
