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

        public async Task<ShoppingCartItem> GetCartItemByIdAsync(int id)
        {
            return await db.ShoppingCartItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await db.Countries.ToListAsync();
        }

        /// <summary>
        /// Checks if this is a new country being added
        /// </summary>
        /// <param name="addNewCountry">Bool to check if a new country is being added.</param>
        /// <param name="CountryName">Name of new country being added.</param>
        /// <returns></returns>
        public async Task<Country> CheckNewCountry(bool addNewCountry, string CountryName, int? WhiskeyCountry)
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

                    await Commit();

                    return customCountry;
                }
                else
                {
                    return await db.Countries.FirstOrDefaultAsync(c => c.Id == WhiskeyCountry);
                }
            }
            return await db.Countries.FirstOrDefaultAsync(c => c.Id == WhiskeyCountry);
        }
    }
}
