using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
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

        public async Task<bool> AddNewPrescriptionAsync()
        {
            this.PrescriptionID = await clsPrescriptionDA.AddNewPrescriptionAsync(this.MedicalRecordID, this.StartDate, this.EndDate);
            return this.PrescriptionID > 0;
        }
    }
}
