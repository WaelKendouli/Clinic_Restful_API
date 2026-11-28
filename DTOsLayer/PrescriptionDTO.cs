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

        public DateTime IssueDate { get; set; }
        public List<DescribedMedicationDTO> liMedications { get; set; } = new List<DescribedMedicationDTO>(); 
        

        public PrescriptionDTO(int prescriptionID, int medicalRecordID, DateTime issueDate , List<DescribedMedicationDTO> Medications)
        {
            PrescriptionID = prescriptionID;
            MedicalRecordID = medicalRecordID;
            IssueDate = issueDate;
            liMedications = Medications;
        }
       
        public PrescriptionDTO()
        {
        }
    }
}
