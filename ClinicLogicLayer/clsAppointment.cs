using DataAccessLayer;
using DTOsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicLogicLayer
{
    public class clsAppointment
    {
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentStatusID { get; set; }

        public clsAppointment(int appointmentID, DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
            AppointmentID = appointmentID;
            Date = date;
            Time = time;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentStatusID = appointmentStatusID;
        }

        public clsAppointment( DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
            Date = date;
            Time = time;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentStatusID = appointmentStatusID;
        }
        public  async Task<bool> AddNewAppointmentAsync(DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
           this.AppointmentID =  await clsAppointmentDA.AddNewAppointmentAsync(date, time, doctorID, patientID, appointmentStatusID);
            return this.AppointmentID > 0;
        }

        public static async Task<bool> ChangeAppointmentStatusToCanceledAsync(int appointmentID)
        { 
            return await clsAppointmentDA.ChangeAppointmentStatusToCanceledAsync(appointmentID);
        }
        public static async Task<List<AppointmentItemDTO>> GetListAppointementByPatientIDAsync(int patientID)
        {
            return await clsAppointmentDA.GetListAppointementByPatientIDAsync(patientID);
        }
    }
}
