using DataAccessLayer;
using DTOsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicLogicLayer
{
    public class clsMedicalRecord
    {
        public int MedicalRecordID { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
        public string AdditionalNotes { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentID { get; set; }


        public clsMedicalRecord( string description, string diagnosis,
            string additionalNotes, int doctorID, int patientID, int appointmentID)
        {
          
            Description = description;
            Diagnosis = diagnosis;
            AdditionalNotes = additionalNotes;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentID = appointmentID;
        }


        public  async Task<bool> AddNewMedicalRecordAsync(MedicalRecordDTO record)
        {
            this.MedicalRecordID = await clsMedicalRecordDA.AddNewMedicalRecordAsync(
                record.Description,
                record.Diagnosis,
                record.AdditionalNotes,
                record.DoctorID,
                record.PatientID,
                record.AppointmentID
            );
            return this.MedicalRecordID > 0;
        }

    }
}
