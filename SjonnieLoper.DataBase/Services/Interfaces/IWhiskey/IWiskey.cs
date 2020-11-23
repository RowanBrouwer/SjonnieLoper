using System;
using System.Collections.Generic;
using System.Text;
using SjonnieLoper.Core;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using SjonnieLoper.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SjonnieLoper.DataBase
{
    public interface IWiskey
    {

        public Task<IEnumerable<WhiskeyBase>> GetAllWhiskeys(string name);

        public Task<IEnumerable<WhiskeyBase>> GetAllWhiskeysSearch(string searchName, string searchBrand, string searchCountry, 
            bool searchForType, WhiskeyType searchType,
            bool searchRangeAge, int searchAge1, int searchAge2,
            bool searchRangePrice, decimal searchPrice1, decimal searchPrice2,
            bool searchRangePercent, decimal searchPercent1, decimal searchPercent2,
            bool includeSoftDelete);

        public Task<WhiskeyBase> GetWhiskeyById(int id);

        public Task<WhiskeyBase> UpdateWiskeyAsync(WhiskeyBase UpdatedWhiskey, bool AddNewCountry, string CountryName);

        public Task<WhiskeyBase> AddWhiskeyAsync(WhiskeyBase NewWhiskey, bool AddNewCountry, string CountryName);

        public Task<WhiskeyBase> DeleteWhiskey(int id);

        public Task<int> GetCountOfWhiskeys();

    }
}
