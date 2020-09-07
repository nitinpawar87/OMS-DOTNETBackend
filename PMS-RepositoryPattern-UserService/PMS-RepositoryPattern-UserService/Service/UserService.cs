using PMS_RepositoryPattern.Model;
using PMS_RepositoryPattern.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Service
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userRepository"></param>
        public UserService(IUserRepository _userRepository)
        {
            try
            {
                this.userRepository = _userRepository;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            try
            {
                user.IsLoggedIn = false;
                userRepository.AddUser(user);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUser(string userName, string password)
        {
            try
            {
                return userRepository.GetUser(userName, password);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            try
            {
                return userRepository.GetUsers();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool? IsUserLoggedIn(int Id)
        {
            try
            {
                User user = userRepository.GetUser(Id);

                if (user != null)
                {
                    if (user.IsLoggedIn)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void LogoutUser(int Id)
        {
            try
            {
                userRepository.LogoutUser(Id);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            try
            {
                userRepository.UpdateUser(user);
            }
            catch
            {
                throw;
            }
        }
    }
}
