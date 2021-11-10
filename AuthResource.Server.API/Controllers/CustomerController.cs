using AuthResource.Server.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthResource.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerHistory historyies;

        public CustomerController(CustomerHistory Historyies)
        {
            historyies = Historyies;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAvaliableCustomers()
        {
            return Ok(historyies.customers);
        }
    }
}
