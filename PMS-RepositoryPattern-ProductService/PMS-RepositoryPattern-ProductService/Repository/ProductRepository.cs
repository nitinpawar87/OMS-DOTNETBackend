using Consul;
using PMS_RepositoryPattern.Config;
using PMS_RepositoryPattern.Data;
using PMS_RepositoryPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDBContext _context;        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        public ProductRepository(ProductDBContext _context)
        {
            try
            {
                this._context = _context;
                //var name = "ProductServiceRegistry";
                //var port = 5300;
                //var id = $"{name}:{port}";
                //var client = new ConsulClient();
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

                //var registration = new AgentServiceRegistration()
                //{
                //    Address = "127.0.0.1",
                //    ID = id,
                //    Name = name,
                //    Port = port
                //};

                //client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

                //Console.WriteLine("DataService started...");
                //Console.WriteLine("Press ESC to exit");
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            try
            {
                _context.Add(product);
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
        /// <returns></returns>
        public Product GetProduct(int Id)
        {
            try
            {
                return _context.Products.Where(product => product.Id == Id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Product GetProduct(string productName)
        {
            try
            {
                return _context.Products.Where(user => user.ProductName.ToLower() == productName.ToLower()).FirstOrDefault();
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
        public IEnumerable<Product> GetProducts()
        {
            try
            {
                return _context.Products;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            try
            {
                _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
