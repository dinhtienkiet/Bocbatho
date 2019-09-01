using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public User GetUserByUserID(int ID)
        {
            var user = DbContext.Users.Where(u => u.UserID == ID).FirstOrDefault();
            return user;
        }

        public User Edit(User record)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var user = DbContext.Users.Where(u => u.UserID == record.UserID).FirstOrDefault();
                    user.Username = record.Username;
                    user.Password = record.Password;
                    user.Avatar = record.Avatar;
                    DbContext.Users.Attach(user);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return record;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }

        }
        public User Login(User user)
        {
            var userLogin = DbContext.Users.
                Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password))
                .FirstOrDefault();
            if (userLogin != null)
            {
                return userLogin;
            }
            return null;

        }

        public User CheckDatabase(int ID, string Username)
        {
            var userTest = DbContext.Users.Where(u => u.UserID == ID && u.Username.Equals(Username)).FirstOrDefault();
            return userTest;
        }
    }
    public interface IUserRepository : IRepository<User>
    {
        User Edit(User record);
        User GetUserByUserID(int ID);
        User Login(User user);
        User CheckDatabase(int ID, string Username);
    }
}