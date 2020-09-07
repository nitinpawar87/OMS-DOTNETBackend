using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMS_RepositoryPattern.Model;
using PMS_RepositoryPattern.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS_RepositoryPattern.Controllers
{
    [ApiVersion("3.0")]
    [Route("product")]
    [ApiController]
    public class ProductV3Controller : ControllerBase
    {
        IProductServiceV2 productService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productService"></param>
        public ProductV3Controller(IProductServiceV2 _productService)
        {
            try
            {
                this.productService = _productService;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }
        // GET: api/<ProductController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUserLoggedIn"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(bool isUserLoggedIn)
        {
            throw new Exception("No Data Available");
        }

       
    }
}
