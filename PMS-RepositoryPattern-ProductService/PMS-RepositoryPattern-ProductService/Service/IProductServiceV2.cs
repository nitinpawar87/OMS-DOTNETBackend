using PMS_RepositoryPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Service
{
   public interface IProductServiceV2
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int Id);
        Product GetProduct(string productName);
        void AddProduct(Product Product);
        void UpdateProduct(Product Product);       
    }
}
