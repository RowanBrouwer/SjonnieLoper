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

        public IEnumerable<WhiskeyBase> GetAllWhiskeys(string name)
        {
            var query = from w in db.whiskeys
                        where w.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby w.Name
                        select w;
            return query;
        }

        public int GetCountOfSpecificWhiskey(int id)
        {
            return GetWhiskeyById(id).AmountInStorage;
        }

        public int GetCountOfWhiskeys()
        {
            return db.whiskeys.Count();
        }

        public ApplicationUser GetUserByName(string name)
        {
            return db.applicationUsers.FirstOrDefault(a => a.UserName == name);
        }

        public WhiskeyBase GetWhiskeyById(int id)
        {
            return db.whiskeys.FirstOrDefault(w => w.Id == id);
        }

        public ApplicationUser UpdateUserInfo(ApplicationUser updatedUser)
        {
            var entity = db.applicationUsers.Attach(updatedUser);
            entity.State = EntityState.Modified;
            return updatedUser;
        }

        public WhiskeyBase UpdateWiskey(WhiskeyBase UpdatedWhiskey)
        {
            var entity = db.whiskeys.Attach(UpdatedWhiskey);
            entity.State = EntityState.Modified;
            return UpdatedWhiskey;
        }
    }
}
