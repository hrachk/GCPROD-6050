using AuthResource.Server.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthResource.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly CustomerHistory historyes;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public HistoryController(CustomerHistory Historyes)
        {
            historyes = Historyes;
        }

        [HttpGet]
        [Authorize (Roles ="User")]
        [Route("")]
        public IActionResult GetHistory()
        {
            if (!historyes.history.ContainsKey(UserId))
                return Ok(Enumerable.Empty<Customer>());

            var orderedCustomerId = historyes.history.Single(o => o.Key == UserId).Value;
            var orderedHistory = historyes.customers.Where(h => orderedCustomerId.Contains(h.CustomerID));

            return Ok(orderedHistory);
            
        }
    }
}
