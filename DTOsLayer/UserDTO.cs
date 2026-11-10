using DTOsLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class UserDTO : IF_User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Role { get; set; }

        // 1. Empty Constructor (Good practice to keep for serializers/ORM frameworks)
        public UserDTO()
        {
        }

        // 2. Constructor for CREATING a new user (No UserID yet)
        public UserDTO(string username, string hashedPassword, string firstName, string lastName,
                       string email, string phone, bool gender, DateTime dateOfBirth, string Role)
        {
            Username = username;
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            this.Role = Role;
        }

        public UserDTO(int userID,string username, string hashedPassword, string firstName, string lastName,
                       string email, string phone, bool gender, DateTime dateOfBirth, string Role)
        {
            UserID = userID;
            Username = username;
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            this.Role = Role;
        }

    }
}
