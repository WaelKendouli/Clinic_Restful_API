using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicLogicLayer
{
    public class clsPatient
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public clsPatient(int patientID, string firstName, string lastName,
            DateTime dateOfBirth, string email, string phone)
        {
            PatientID = patientID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
        }

        public static async Task<List<clsPatient>> GetAllPatientsAsListAsync()
        {
            return await clsPatient.GetAllPatientsAsListAsync();
        }
    }
}
