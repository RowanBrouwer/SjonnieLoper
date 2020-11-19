using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using System.Threading.Tasks;

namespace SjonnieLoper.DataBase.Services.Interfaces
{
    public class SQLAppUser : IAppUser
    {
        private readonly ApplicationDbContext db;
        private readonly IGeneral general;

        public SQLAppUser(ApplicationDbContext db, IGeneral general)
        {
            this.db = db;
            this.general = general;
        }

        public async Task<ApplicationUser> AddUser(ApplicationUser NewUser)
        {
            await db.AddAsync(NewUser);
            return NewUser;
        }

        /// <summary>
        /// Soft deletes an user by UserName
        /// </summary>
        public async Task<ApplicationUser> DeleteUser(string name)
        {
            var appuser = await GetUserByNameAsync(name);
            if (appuser != null)
            {
                appuser.SoftDeleted = true;
            }
            return appuser;
        }

        /// <summary>
        /// Gets an ApplicationUser by UserName
        /// </summary>
        public async Task<ApplicationUser> GetUserByNameAsync(string name)
        {
            return await db.ApplicationUsers.FirstOrDefaultAsync(a => a.UserName == name);
        }

        public ApplicationUser UpdateUserInfo(ApplicationUser updatedUser)
        {
            var entity = db.ApplicationUsers.Attach(updatedUser);
            entity.State = EntityState.Modified;
            return updatedUser;
        }
    }
}
