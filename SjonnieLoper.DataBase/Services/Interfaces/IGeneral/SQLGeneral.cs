using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core.Models;
using SjonnieLoper.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public class SQLGeneral : IGeneral
    {
        private readonly ApplicationDbContext db;

        public SQLGeneral(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Commit()
        {
            int PB = await db.SaveChangesAsync();
            return PB;
        }

        /// <summary>
        /// Gets a cartItem by Id.
        /// </summary>
        public async Task<ShoppingCartItem> GetCartItemByIdAsync(int id)
        {
            return await db.ShoppingCartItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Gets a IEnumerable with all countries in the database.
        /// </summary>
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await db.Countries.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addNewCountry">Bool to check if a new country is being added.</param>
        /// <param name="countryName">Name of new country being added.</param>
        /// <param name="whiskeyCountryId"></param>
        /// <returns></returns>
        public async Task<Country> CheckNewCountry(bool addNewCountry, string countryName, int whiskeyCountryId)
        {
            if (addNewCountry)
            {
                if ((await db.Countries.FirstOrDefaultAsync(c => c.Name == countryName)) == null)
                {
                    Country customCountry = new Country
                    {
                        Name = countryName
                    };
                    db.Add(customCountry);

                    await Commit();

                    return customCountry;
                }
                else
                {
                    return await db.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
                }
            }
            return await db.Countries.FirstOrDefaultAsync(c => c.Id == whiskeyCountryId);
        }
    }
}
