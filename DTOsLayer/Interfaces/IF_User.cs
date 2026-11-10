using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer.Interfaces
{
    public interface IF_User
    {
         int UserID { get; set; }
         string Username { get; set; } 
         string HashedPassword { get; set; } 
         string FirstName { get; set; } 
         string LastName { get; set; } 
         string Email { get; set; } 
         string Phone { get; set; }
         bool Gender { get; set; }
         DateTime DateOfBirth { get; set; }
         string Role { get; set; }
    }
}
