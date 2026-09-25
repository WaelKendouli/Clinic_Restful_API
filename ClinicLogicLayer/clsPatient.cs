using DataAccessLayer;
using DTOsLayer;
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

        public string Gender { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public clsPatient(int patientID, string firstName, string lastName,
            DateTime dateOfBirth, string email, string Adress, string phone , string gender, string password)
        {
            PatientID = patientID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
            this.Address = Address;
            this.Gender = gender;
            this.Password = password;
        }

        public clsPatient( string firstName, string lastName,
            DateTime dateOfBirth, string email, string address ,string phone , string gender, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
            this.Address = address;
            this.Gender = gender;
            this.Password = password;
        }

        public  async Task<int> AddNewPatient()
        {
          this.PatientID = await  PatientsDA.AddNewPatientAsync(this.FirstName, this.LastName, this.DateOfBirth,
              this.Phone, this.Email,
              this.Address, this.Gender, this.Password);
            return this.PatientID;
        }

        public static async Task<List<PatientDTO>> GetAllPatientsAsListAsync()
        {
            return await PatientsDA.GetAllPatientsAsListAsync();
        }

        public static async Task<bool> DeletePatientAsync(int patientID)
        {
            return await PatientsDA.DeletePatientAsync(patientID);
        }
    }
}
