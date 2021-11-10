using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthResource.Server.API.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string CustDescription { get; set; }
        

    }
}
