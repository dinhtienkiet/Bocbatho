using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository UserRepository, IUnitOfWork unitOfWork)
        {
            this.UserRepository = UserRepository;
            this.unitOfWork = unitOfWork;

        }
        public void Edit(User user)
        {
            UserRepository.Edit(user);
            SaveUser();
        }
        public User GetUserByUserID(int ID)
        {
            return UserRepository.GetUserByUserID(ID);
        }
        public void SaveUser()
        {
            unitOfWork.Commit();
        }
        public User Login(User user)
        {
            var userTest = UserRepository.Login(user);
            return userTest;
        }

        public User CheckDatabase(int ID, string Username)
        {
            return UserRepository.CheckDatabase(ID,Username);
        }
    }
    public interface IUserService
    {
        User Login(User user);
        User GetUserByUserID(int ID);
        User CheckDatabase(int ID, string Username);
        void Edit(User user);
    }
}