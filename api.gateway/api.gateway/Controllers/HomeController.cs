using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot;
using Microsoft.AspNetCore.Mvc;
using Consul;

namespace api.gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Uri> _serverUrls = new List<Uri>();
            var consuleClient = new ConsulClient(c => c.Address = new Uri("http://127.0.0.1:8500"));
            var services = consuleClient.Agent.Services().Result.Response;
            foreach (var service in services)
            {
                var isSchoolApi = service.Value.Tags.Any(t => t == "School") &&
                                  service.Value.Tags.Any(t => t == "Students");
                if (isSchoolApi)
                {
                    var serviceUri = new Uri($"{service.Value.Address}:{service.Value.Port}");
                    _serverUrls.Add(serviceUri);
                }
            }
            return Content("Gateway started successfully at " + DateTime.Now.ToString());
        }
    }
}
