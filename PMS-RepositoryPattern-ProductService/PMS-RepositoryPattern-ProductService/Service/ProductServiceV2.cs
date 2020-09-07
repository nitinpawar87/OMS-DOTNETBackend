using PMS_RepositoryPattern.Model;
using PMS_RepositoryPattern.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_RepositoryPattern.Service
{
    public class ProductServiceV2 : IProductServiceV2
    {
        private IProductRepository productRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productRepository"></param>
        public ProductServiceV2(IProductRepository _productRepository)
        {
            try
            {
                this.productRepository = _productRepository;
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
                productRepository.AddProduct(product);
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
                Product product = productRepository.GetProduct(Id);
                if (product != null)
                {
                    if (product.ProductVersion == "2.0")
                    {
                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }
                return product;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public Product GetProduct(string productName)
        {
            try
            {
                //   return productRepository.GetProduct(productName);
                Product product = productRepository.GetProduct(productName);
                if (product != null)
                {
                    if (product.ProductVersion == "2.0")
                    {
                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }
                return product;
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
                return productRepository.GetProducts().Where(p => p.ProductVersion == "2.0");
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
                productRepository.UpdateProduct(product);
            }
            catch
            {
                throw;
            }
        }
    }
}
