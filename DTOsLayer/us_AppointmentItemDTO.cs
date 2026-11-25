using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class us_AppointmentItemDTO
    {
        /// <summary>
        /// for sipalying purposes only used in patient UI parts
        /// </summary>
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public string Status { get; set; }   // was "Stauts"
        public string Service { get; set; }
        public int PatientID { get; set; }
        public string PatientFullName { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public us_AppointmentItemDTO()
        {
        }

        public us_AppointmentItemDTO(
            int appointmentID,
            DateTime date,
            string doctorName,
            string status,
            string service,
            int patientID,
            TimeSpan start,
            TimeSpan end)
        {
            AppointmentID = appointmentID;
            Date = date;
            DoctorName = doctorName;
            Status = status;
            Service = service;
            PatientID = patientID;
            Start = start;
            End = end;
        }

        public us_AppointmentItemDTO(
            int appointmentID,
            DateTime date,
            string doctorName,
            string status,
            string service,
            int patientID,
            TimeSpan start,
            TimeSpan end,
            string PatientName)
        {
            AppointmentID = appointmentID;
            Date = date;
            Status = status;
            Service = service;
            PatientID = patientID;
            Start = start;
            End = end;
            this.PatientFullName = PatientName;
        }

    }
}
