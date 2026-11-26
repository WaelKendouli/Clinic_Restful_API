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
      public enum eSatuses {  Scheduled = 1 , Confirmed = 2 , CheckedIn = 3 , InProgress = 4 ,
            Completed = 5 , Cancelled =6 ,NoShow = 7 , Rescheduled = 8}

        public static eSatuses status { get; set; } = new eSatuses();
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
        public static  async Task<bool> AddNewAppointmentAsync(AppointmentDTO appointment)
        {
           int AppointmentID =  await clsAppointmentDA.AddNewAppointmentAsync(appointment);
            return AppointmentID > 0;
        }

        public static async Task<bool> ChangeAppointmentStatusToCanceledAsync(int appointmentID)
        { 
            return await clsAppointmentDA.ChangeAppointmentStatusToCanceledAsync(appointmentID);
        }
        public static async Task<List<us_AppointmentItemDTO>> GetListAppointementByPatientIDAsync(int patientID)
        {
            return await clsAppointmentDA.GetListAppointementByPatientIDAsync(patientID);
        }

        public static async Task<bool> UpdateAppointmentAsync(int appointmentID, DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
            return await clsAppointmentDA.UpdateAppointmentAsync(appointmentID, date, time, doctorID, patientID, appointmentStatusID);
        }
        public static async Task<AppointmentDTO> FindAppointmentByIDAsync(int appointmentID)
        {
            return await clsAppointmentDA.FindAppointmentByIDAsync(appointmentID);
        }
        public static async Task<List<us_AppointmentItemDTO>> GetListOfCancelledAppointmentsAsync(int patientID)
        {
            return await clsAppointmentDA.GetListOfCancelledAppointmentsAsync(patientID);
        }

        public static async Task<bool> ChangeAppointmentStatusAsync(int newAppointmentStatusID, int appointmentID)
        {
            return await clsAppointmentDA.ChangeAppointmentStatusAsync(newAppointmentStatusID, appointmentID);
        }

    }
}
