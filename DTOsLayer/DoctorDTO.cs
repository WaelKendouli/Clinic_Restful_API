using DTOsLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DoctorDTO : UserDTO , IF_User
    {
        // Properties specific to the Doctors table
        public int DoctorID { get; set; }
        public string PhotoURL { get; set; }
        public int SpecializationID { get; set; }

        public string Specialization { get; set; }

        public string Bio { get; set; } // Added based on your database image

        public DoctorScheduelDTO Scheduel { get; set; } = new DoctorScheduelDTO();

        // Empty Constructor for serializers/ORM
        public DoctorDTO() : base()
        {
        }

        // Constructor for creating a DoctorDTO
        // Note: Since DoctorDTO inherits from UserDTO, we pass the User-related data 
        // to the base constructor using the `: base(...)` syntax.
        public DoctorDTO(
            int doctorID,
            string photoURL,
            int specializationID,
            string bio,
            string specialization ,
            // User properties below:
            int userID,
            string username,
            string hashedPassword,
            string firstName,
            string lastName,
            string email,
            string phone,
            bool gender,
            DateTime dateOfBirth,
            string role )
            : base(userID, username, hashedPassword, firstName, lastName, email, phone, gender, dateOfBirth, role)
        {
            DoctorID = doctorID;
            PhotoURL = photoURL;
            SpecializationID = specializationID;
            Bio = bio;
            Specialization = specialization;
        }

        public DoctorDTO(
       int doctorID,
       string photoURL,
       int specializationID,
       string specialization,
       string bio, // <-- Added Schedule object here  // User properties below:
       int userID,
       string username,
       string hashedPassword,
       string firstName,
       string lastName,
       string email,
       string phone,
       bool gender,
       DateTime dateOfBirth,
       string role , DoctorScheduelDTO scheduel
       )
       : base(userID, username, hashedPassword, firstName, lastName, email, phone, gender, dateOfBirth, role)
        {
            DoctorID = doctorID;
            PhotoURL = photoURL;
            SpecializationID = specializationID;
            Specialization = specialization;
            Bio = bio;
            Scheduel = scheduel ; 
        }



    }
}
