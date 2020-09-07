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
    [ApiVersion("2.0")]
    [Route("product")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        IProductServiceV2 productService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productService"></param>
        public ProductV2Controller(IProductServiceV2 _productService)
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
        public IActionResult Get(bool isUserLoggedIn)
        {
            try
            {
                if (isUserLoggedIn)
                {

                    return Ok(productService.GetProducts());
                }
                else
                {

                  return BadRequest("The requested action cannot be performed as user has not logged in ");
              
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUserLoggedIn"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduct")]
        public IActionResult Get(bool isUserLoggedIn, int Id)
        {
            try
            {
                if (isUserLoggedIn)
                {

                    Product product = productService.GetProduct(Id);
                    if (product == null)
                    {
                        return NotFound("No records found...");
                    }
                    else
                    {
                        return Ok(product);
                    }
                }
                else
                {
                    return BadRequest("The requested action cannot be performed as user has not logged in ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUserLoggedIn"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ProductwithName")]
        public IActionResult GetWithProductName(bool isUserLoggedIn, string productName)
        {
            try
            {
                if (isUserLoggedIn)
                {

                    Product product = productService.GetProduct(productName);
                    if (product == null)
                    {
                        return NotFound("No records found...");
                    }
                    else
                    {
                        return Ok(product);
                    }
                }
                else
                {
                    return BadRequest("The requested action cannot be performed as user has not logged in ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUserLoggedIn"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post(bool isUserLoggedIn, [FromBody] Product product)
        {
            try
            {
                if (isUserLoggedIn)
                {

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    if (product.ProductVersion == "2.0")
                    {
                        productService.AddProduct(product);
                        return Content("Product added successfully");// StatusCode(StatusCodes.Status201Created);
                    }
                    else
                    {
                        return Content("Product was not added due to product version mismatch");
                    }
                }
                else
                {
                    return BadRequest("The requested action cannot be performed as user has not logged in ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUserLoggedIn"></param>
        /// <param name="Id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        // PUT api/<ProductController>/5    
        [HttpPut]
        public IActionResult Put(bool isUserLoggedIn, int Id, [FromBody] Product product)
        {
            try
            {
                if (isUserLoggedIn)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    if (Id != product.Id)
                    {
                        return BadRequest("Incorrect data provided");
                    }

                    if (product.ProductVersion == "2.0")
                    {
                        productService.UpdateProduct(product);
                        return Content("Product updated successfully");// StatusCode(StatusCodes.Status201Created);
                    }
                    else
                    {
                        return BadRequest("Product was not updated due to product version mismatch");
                    }
                }
                else
                {
                    return BadRequest("The requested action cannot be performed as user has not logged in ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
