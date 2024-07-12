using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Auth
{
    public class LoginedUserDto
    {
        
        public string Email { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string Token { get; set; }
    }
}