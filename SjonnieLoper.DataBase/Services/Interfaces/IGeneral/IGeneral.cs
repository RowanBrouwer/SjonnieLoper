using SjonnieLoper.Core.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public interface IGeneral
    {
        public Task<IEnumerable<Country>> GetAllCountriesAsync();

        public Task<int> Commit();

        public Task<ShoppingCartItem> GetCartItemByIdAsync(int id);
        
        public Task<Country> CheckNewCountry(bool addNewCountry, string CountryName, int? WhiskeyCountry);
    }
}
