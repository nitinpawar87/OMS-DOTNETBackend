using PMS_RepositoryPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Service
{
   public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(string userName, string password);
        void AddUser(User user);
        void UpdateUser(User user);
        void LogoutUser(int Id);
        bool? IsUserLoggedIn(int Id);
    }
}
