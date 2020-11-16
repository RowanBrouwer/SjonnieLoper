using SjonnieLoper.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public interface IAppUser
    {
        public Task<ApplicationUser> GetUserByNameAsync(string name);
        public ApplicationUser UpdateUserInfo(ApplicationUser updatedUser);
        public Task<ApplicationUser> AddUser(ApplicationUser NewUser);
        public Task<ApplicationUser> DeleteUser(string name);

    }
}
