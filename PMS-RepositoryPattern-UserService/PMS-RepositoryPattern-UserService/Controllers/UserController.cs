using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMS_RepositoryPattern.Data;
using PMS_RepositoryPattern.Model;
using PMS_RepositoryPattern.Service;

namespace PMS_RepositoryPattern.Controllers
{
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private readonly IHttpClientFactory _httpClientFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        //infered body members
        public UserController(IUserService _userService, IHttpClientFactory httpClientFactory)
        {
            this.userService = _userService; 
            this._httpClientFactory = httpClientFactory;
        }       
        //public UserController(IUserService _userService)
        //{
        //    this.userService = _userService;
        //}
        public IActionResult Get()
        {
            try
            {               
                return Ok(userService.GetUsers());
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
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("Login")]
        public IActionResult GetUser(string userName, string password)

        {
            try
            {

                User user = userService.GetUser(userName, password);
                if (user == null)
                {
                    return NotFound("No records found...");
                }
                else
                {
                    //tuple decontsruction
                    //discard
                    var (Id, name, officeName, _) = GetUserData(user);
                    Console.WriteLine("user details:");
                    Console.WriteLine("ID: " + Id.ToString() + " Name: " + name + " Sales Office Name" + officeName);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<string>>> GetProducts(string userName, string password)

        {
            try
            {                
                User user = userService.GetUser(userName, password);


                if (user == null)
                {
                    return NotFound("No records found...");
                }
                else
                {
                    //tuple decontsruction
                    //discard
                    var (Id, name, officeName, _) = GetUserData(user);
                    Console.WriteLine("user details:");
                    Console.WriteLine("ID: " + Id.ToString() + " Name: " + name + " Sales Office Name" + officeName);
                    var client = _httpClientFactory.CreateClient("errorApi");
                   var response = await client.GetAsync("product?api-version=3.0&IsUserloggedin=true");

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return BadRequest(await response.Content.ReadAsStringAsync());
                    }
                      //return JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
                   // return response.Content .ReadAsStringAsync();
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
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                userService.AddUser(user);
                return Content("User added successfully");// StatusCode(StatusCodes.Status201Created);
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
        /// <param name="Id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //[Route("Logout"]
        [HttpPut]
        public IActionResult Put(int Id, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (Id != user.Id)
                {
                    return BadRequest("Incorrect data provided");
                }

                userService.UpdateUser(user);
                return Content("User updated successfully");// StatusCode(StatusCodes.Status201Created);
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
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Logout")]
        public IActionResult LogoutUser(int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                userService.LogoutUser(Id);
                return Content("User logged out successfully");// StatusCode(StatusCodes.Status201Created);
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
        /// <param name="user"></param>
        /// <returns></returns>
        private static (int, string, string, bool) GetUserData(User user)
        {
            try
            {
                return (user.Id, user.UserName, user.SalesOfficeName, user.IsLoggedIn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (0, string.Empty, string.Empty, false);
            }
        }
    }
}
