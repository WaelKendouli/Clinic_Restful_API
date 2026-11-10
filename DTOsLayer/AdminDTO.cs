using DTOsLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class AdminDTO : UserDTO , IF_User
    {
        public int AdminID { get; set; }

        // 1. Empty ctor for serializers / ORM
        public AdminDTO() { }

        // 2. Ctor for CREATING a new admin (no IDs yet)
        public AdminDTO(
            string username,
            string hashedPassword,
            string firstName,
            string lastName,
            string email,
            string phone,
            bool gender,
            DateTime dateOfBirth)
            : base(username, hashedPassword, firstName, lastName,
                   email, phone, gender, dateOfBirth, "Admin")
        {
        }

        // 3. Ctor for READING an existing admin (has IDs)
        public AdminDTO(
            int adminID,
            int userID,
            string username,
            string hashedPassword,
            string firstName,
            string lastName,
            string email,
            string phone,
            bool gender,
            DateTime dateOfBirth)
            : base(userID, username, hashedPassword, firstName, lastName,
                   email, phone, gender, dateOfBirth, "Admin")
        {
            AdminID = adminID;
        }
    }
}
