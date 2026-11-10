using DTOsLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class PatientDTO : UserDTO , IF_User
    {
        public int PatientID { get; set; }
        public string Address { get; set; } = string.Empty;

        // 1. Empty ctor for serializers/ORM
        public PatientDTO() { }

        // 2. Ctor for CREATING a new patient (no IDs yet)
        //    Matches the UserDTO "create" ctor signature exactly, then adds Address.
        public PatientDTO(
            string username,
            string hashedPassword,
            string firstName,
            string lastName,
            string email,
            string phone,
            bool gender,
            DateTime dateOfBirth,
            string address)
            : base(username, hashedPassword, firstName, lastName,
                   email, phone, gender, dateOfBirth, "Patient")
        {
            Address = address;
        }

        // 3. Ctor for READING an existing patient (has IDs)
        public PatientDTO(
            int patientID,
            int userID,
            string username,
            string hashedPassword,
            string firstName,
            string lastName,
            string email,
            string phone,
            bool gender,
            DateTime dateOfBirth,
            string address)
            : base(userID, username, hashedPassword, firstName, lastName,
                   email, phone, gender, dateOfBirth, "Patient")
        {
            PatientID = patientID;
            Address = address;
        }
    }
}
