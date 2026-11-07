using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class LoginTestClassDTO
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        
        public LoginTestClassDTO(string username, string password)
        {
            Username = username;
            Password = password;
            
        }
    }
}
