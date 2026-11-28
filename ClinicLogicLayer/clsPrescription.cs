using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOsLayer;
namespace ClinicLogicLayer
{
    public class clsPrescription
    {
        public int PrescriptionID { get; set; }
        public int MedicalRecordID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public clsPrescription( int medicalRecordID, DateTime startDate, DateTime endDate)
        {
            MedicalRecordID = medicalRecordID;
            StartDate = startDate;
            EndDate = endDate;
        }

        public static async Task<bool> AddNewPrescriptionAsync(PrescriptionDTO DTO)
        {
            int PrescriptionID = await clsPrescriptionDA.AddNewPrescriptionAsync(DTO);
            return PrescriptionID > 0;
        }
    }
}
