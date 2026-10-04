using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class PrescriptionDTO
    {
        public int PrescriptionID { get; set; }
        public int MedicalRecordID { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PrescriptionDTO(int prescriptionID, int medicalRecordID, DateTime startDate, DateTime endDate)
        {
            PrescriptionID = prescriptionID;
            MedicalRecordID = medicalRecordID;
            StartDate = startDate;
            EndDate = endDate;
        }
        public PrescriptionDTO( int medicalRecordID, DateTime startDate, DateTime endDate)
        {
            MedicalRecordID = medicalRecordID;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
