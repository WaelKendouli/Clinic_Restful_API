using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace ClinicLogicLayer
{
    public class clsDoctor
    {

        enum enMode { AddNew = 1 , Update = 2 }

        enMode mode;
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

        public clsDoctor(int ID,string firstName, string lastName,
            DateTime dateOfBirth, string phone, string email, string address,
            string gender, string photoURL, int specializationID)
        {
            mode = enMode.Update;
            DoctorID = ID;
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
        public clsDoctor( string firstName, string lastName,
            DateTime dateOfBirth, string phone, string email, string address,
            string gender, string photoURL, int specializationID)
        {
            mode = enMode.AddNew;
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

        private async Task<int> AddNew()
        { 
            return await clsDoctorsDA.AddNewDoctorAsync(FirstName, LastName, DateOfBirth, Phone, Email, Address, Gender, PhotoURL, SpecializationID);
        }

        public async Task<bool> Save()
        {
            switch(mode)
            {
                case enMode.AddNew:
                    DoctorID = await AddNew();
                    return DoctorID > 0;

                 
            }
            return false;
        }
    }
}
