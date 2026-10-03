using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class MedicalRecordDTO
    {
        public int MedicalRecordID { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
        public string AdditionalNotes { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentID { get; set; }

        public MedicalRecordDTO(int medicalRecordID, string description, string diagnosis,
        string additionalNotes, int doctorID, int patientID, int appointmentID)
        {
            MedicalRecordID = medicalRecordID;
            Description = description;
            Diagnosis = diagnosis;
            AdditionalNotes = additionalNotes;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentID = appointmentID;
        }
    }
}
