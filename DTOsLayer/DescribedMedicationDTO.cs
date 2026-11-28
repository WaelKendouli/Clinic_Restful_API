using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DescribedMedicationDTO
    {
        public int ID { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public string Dosage { get; set; }        
        public string Frequncy { get; set; }        
        public DateTime? StartDate { get; set; }    
        public DateTime? EndDate { get; set; }      

       
        public DescribedMedicationDTO()
        {
            Frequncy = string.Empty;
        }

        // Parameterized constructor
        public DescribedMedicationDTO(int id, int prescriptionID, int medicationID,
                                      string dosage, string frequncy,
                                      DateTime? startDate, DateTime? endDate)
        {
            ID = id;
            PrescriptionID = prescriptionID;
            MedicationID = medicationID;
            Dosage = dosage;
            Frequncy = frequncy;
            StartDate = startDate;
            EndDate = endDate;
        }
        public DescribedMedicationDTO( int prescriptionID, int medicationID,
                                     string dosage, string frequncy,
                                     DateTime? startDate, DateTime? endDate)
        {
            
            PrescriptionID = prescriptionID;
            MedicationID = medicationID;
            Dosage = dosage;
            Frequncy = frequncy;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
