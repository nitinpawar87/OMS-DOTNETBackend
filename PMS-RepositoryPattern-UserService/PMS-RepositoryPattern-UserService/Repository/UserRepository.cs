using Consul;
using PMS_RepositoryPattern.Data;
using PMS_RepositoryPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Repository
{
    public class UserRepository : IUserRepository
    {
        ProductDBContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        public UserRepository(ProductDBContext _context)
        {
            try
            {
                this._context = _context;
                /* 
                  var client = new ConsulClient();

                  var name = "UserServiceRegistry";
                  var port = 50616;
                  var id = $"{name}:{port}";                

                  //var tcpCheck = new AgentServiceCheck()
                  //{
                  //    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                  //    Interval = TimeSpan.FromSeconds(30),
                  //    TCP = $"127.0.0.1:{port}"
                  //};

                  //var httpCheck = new AgentServiceCheck()
                  //{
                  //    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                  //    Interval = TimeSpan.FromSeconds(30),
                  //    HTTP = $"http://127.0.0.1:{port}/HealthCheck"
                  //};

                  var registration = new AgentServiceRegistration()
                  {                   
                      Address = "localhost",
                      ID = id,
                      Name = name,
                      Port = port

                  };

                  client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

                  Console.WriteLine("DataService started...");
                  Console.WriteLine("Press ESC to exit"); */

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
                _context.Users.Add(user);
                _context.SaveChanges();
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
                User user = _context.Users.Where(user => user.UserName.ToLower() == userName.ToLower() && user.Password == password).FirstOrDefault();
                if (user != null)
                {
                    user.IsLoggedIn = true;
                    _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }
                return _context.Users.Where(user => user.UserName.ToLower() == userName.ToLower() && user.Password == password).FirstOrDefault();
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
                return _context.Users;
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
                _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
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
                User user = _context.Users.Where(user => user.Id == Id).FirstOrDefault();
                if (user != null)
                {
                    user.IsLoggedIn = false;
                    _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
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
        /// <returns></returns>
        public User GetUser(int Id)
        {
            try
            {
                return _context.Users.Where(user => user.Id == Id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async Task RegisterService()
        {
            var client = new ConsulClient();
            var agentReg = new AgentServiceRegistration()
            {
                Address = "127.0.0.1",
                ID = "userReg",
                Name = "serviceName",
                Port = 5200
            };

            await client.Agent.ServiceRegister(agentReg);
        }
    }
}
