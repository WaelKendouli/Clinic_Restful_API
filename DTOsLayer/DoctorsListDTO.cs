using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DoctorsListDTO : DoctorDTO
    {
       public string specialization { get; set; }
        public DoctorsListDTO(int doctorID, string firstName, string lastName, string gender, string phone, string email, string Adress, string PhotoURL, int SpecializationID, string specialization , DateTime date) : base(doctorID, firstName, lastName, date , phone, email, Adress , gender, PhotoURL, SpecializationID)
        {
            
            this.specialization = specialization;
        }
    }
}
