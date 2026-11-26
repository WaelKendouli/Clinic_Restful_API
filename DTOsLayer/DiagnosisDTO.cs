using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DiagnosisDTO
    {
        public int DiagnosisID { get; set; }
        public int MedicalRecordID { get; set; }
        public string Conclution { get; set; }   // NOT NULL
        public string ICD10Code { get; set; }     // UNIQUE + NOT NULL

        // Parameterless constructor
        public DiagnosisDTO()
        {
            Conclution = string.Empty;
            ICD10Code = string.Empty;
        }

        // Parameterized constructor
        public DiagnosisDTO(int diagnosisID, int medicalRecordID, string conclution, string icd10Code)
        {
            DiagnosisID = diagnosisID;
            MedicalRecordID = medicalRecordID;
            Conclution = conclution;
            ICD10Code = icd10Code;
        }
    }
}
