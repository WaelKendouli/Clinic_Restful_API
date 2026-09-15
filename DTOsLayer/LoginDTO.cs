using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class LoginDTO
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public LoginDTO(string username, string password, string phone = "", string email = "")
        {
            Username = username;
            Password = password;
            Phone = phone;
            Email = email;
        }
    }
}
