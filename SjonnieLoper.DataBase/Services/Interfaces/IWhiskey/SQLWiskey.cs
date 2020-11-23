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
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.DataBase
{
    public class SQLWiskey : IWiskey
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneral general;

        public SQLWiskey(ApplicationDbContext db, IGeneral general)
        {
            this.db = db;
            this.general = general;
        }

        /// <summary>
        /// Adds whiskey async
        /// </summary>
        /// <param name="NewWhiskey"></param>
        /// <param name="addNewCountry">Bool to check if a new country is being added.</param>
        /// <param name="CountryName">Name of new country being added.</param>
        /// <returns></returns>
        public async Task<WhiskeyBase> AddWhiskeyAsync(WhiskeyBase NewWhiskey, bool addNewCountry, string CountryName)
        {
            var WhiskeyCountry = NewWhiskey.CountryOfOrigin.Id;
            NewWhiskey.CountryOfOrigin = await general.CheckNewCountry(addNewCountry, CountryName, WhiskeyCountry);

            if (NewWhiskey.CountryOfOrigin != null)
                whiskeyCountryId = NewWhiskey.CountryOfOrigin.Id;

            NewWhiskey.CountryOfOrigin = await _general.CheckNewCountry(addNewCountry, countryName, whiskeyCountryId);

            db.Add(NewWhiskey);

            return NewWhiskey;
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


        /// <summary>
        /// General query for the basic search.
        /// </summary>
        public async Task<IEnumerable<WhiskeyBase>> GetAllWhiskeys(string name)
        {

            var query = from w in db.Whiskeys
                        where w.SoftDeleted == false
                        where string.IsNullOrEmpty(name) || w.Name.Contains(name) || w.Brand.Contains(name) || w.CountryOfOrigin.Name.Contains(name)
                        orderby w.Name
                        select w;

            return await query.ToListAsync();
        }

        /// <summary>
        /// Query for the advanced search.
        /// </summary>
        public async Task<IEnumerable<WhiskeyBase>> GetAllWhiskeysSearch(string searchName, string searchBrand, string searchCountry, 
            bool searchForType, WhiskeyType searchType,
            bool searchRangeAge, int searchAge1, int searchAge2,
            bool searchRangePrice, decimal searchPrice1, decimal searchPrice2,
            bool searchRangePercent, decimal searchPercent1, decimal searchPercent2,
            bool includeSoftDelete)
        {
            var query = from w in db.Whiskeys
                        where (includeSoftDelete || w.SoftDeleted == false)
                        where (string.IsNullOrEmpty(searchName) || w.Name.Contains(searchName))
                        where (string.IsNullOrEmpty(searchBrand) || w.Brand.Contains(searchBrand))
                        where (string.IsNullOrEmpty(searchCountry) || w.CountryOfOrigin.Name.Contains(searchCountry))
                        where (!searchForType || w.Type == searchType)
                        where (!searchRangeAge && (searchAge1 == 0 || w.AgeYears == searchAge1) || (w.AgeYears >= searchAge1 && w.AgeYears <= searchAge2))
                        where (!searchRangePrice && (searchPrice1 == 0 || w.Price == searchPrice1) || (w.Price >= searchPrice1 && w.Price <= searchPrice2))
                        where (!searchRangePercent && (searchPercent1 == 0 || w.Percentage == searchPercent1) || (w.Percentage >= searchPercent1 && w.Percentage <= searchPercent2))
                        orderby w.Name
                        select w;

            return await query.ToListAsync();
        }

        public async Task<int> GetCountOfWhiskeys()
        {
            return await db.Whiskeys.CountAsync();
        }

        public async Task<WhiskeyBase> GetWhiskeyById(int id)
        {
            return await db.Whiskeys.Include(w => w.CountryOfOrigin).FirstOrDefaultAsync(w => w.Id == id);
        }

        /// <summary>
        /// Stella's update code.
        /// </summary>
        /// <param name="updatedWhiskey"></param>
        /// <param name="addNewCountry">Bool to check if a new country is being added.</param>
        /// <param name="countryName">Name of new country being added.</param>
        /// <returns></returns>
        public async Task<WhiskeyBase> UpdateWiskeyAsync(WhiskeyBase updatedWhiskey, bool addNewCountry, string countryName)
        {
            int whiskeyCountryId = 0;

            if (updatedWhiskey.CountryOfOrigin != null)
                whiskeyCountryId = updatedWhiskey.CountryOfOrigin.Id;

            updatedWhiskey.CountryOfOrigin = await _general.CheckNewCountry(addNewCountry, countryName, whiskeyCountryId);

            entity.State = EntityState.Modified;
            
            return updatedWhiskey;
        }


    }
}
