using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class MedicalRecordAggregateDTO
    {
        // Core record
        public MedicalRecordDTO MedicalRecord { get; set; }
        public VitalsDTO Vitals { get; set; }
        public DiagnosisDTO Diagnosis { get; set; }

        public MedicalRecordAggregateDTO()
        {
            MedicalRecord = new MedicalRecordDTO();
            Vitals = new VitalsDTO();
            Diagnosis = new DiagnosisDTO();
        }

        // Parameterized constructor
        public MedicalRecordAggregateDTO(MedicalRecordDTO medicalRecord,
                                         VitalsDTO vitals,
                                         DiagnosisDTO diagnosis)
        {
            MedicalRecord = medicalRecord;
            Vitals = vitals;
            Diagnosis = diagnosis;
        }
    }
}
