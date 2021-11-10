using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.API.Models
{
    public class Account
    {
        public Guid id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role[] Roles { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}
