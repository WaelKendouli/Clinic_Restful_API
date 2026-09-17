using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DoctorDTO
    {
        public int DoctorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PhotoURL { get; set; }
        public int SpecializationID { get; set; }

       

        public DoctorDTO(int doctorID, string firstName, string lastName,
            DateTime dateOfBirth, string phone, string email, string address,
            string gender, string photoURL, int specializationID)
        {
            DoctorID = doctorID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            Address = address;
            Gender = gender;
            PhotoURL = photoURL;
            SpecializationID = specializationID;
        }
    }
}
