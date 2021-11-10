using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthResource.Server.API.Models
{
    public class CustomerHistory
    {
        // registered users list // from Database
        public List<Customer> customers => new List<Customer> { 
        
        new Customer(){CustomerID =1,  CustomerName = "Ekaterina Ivanova" , Adress = "New York, 114/78A" , Age = 25, CustDescription = "The client is healthy ... " },
        new Customer(){CustomerID =2,  CustomerName = "Andrew K.H" , Adress = "London, str. 3 b.12" , Age = 25, CustDescription = "The client did not have any heart problems during the examination. "},
        new Customer(){CustomerID =3,  CustomerName = "Arnold B.B" , Adress = "Moskva, str. Lenina 114/25" , Age = 25, CustDescription = "The examination revealed a muscle problem"},
        new Customer(){CustomerID =4,  CustomerName = "Natasha A.M" , Adress = "California, str B.652" , Age = 25, CustDescription = "There is an aggravation of the lungs"}

        };

        //
        public Dictionary<Guid, int[]> history => new Dictionary<Guid, int[]> {

             {Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"), new int[]{ 1 } },
             {Guid.Parse("7b0a1ec1-80f5-46b5-a108-fb938d3e26c0"), new int[]{ 2 } }
        };
    }
}
