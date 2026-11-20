using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class AppointmentDTO
    {
       public int AppointmentID { get; set; }
        public DateTime IssueDate { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentStatusID { get; set; }
        public int ServiceID { get; set; }
        public ServiceDTO Serivce { get; set; } = new ServiceDTO();

        public AppointmentDTO()
        {
            
        }
        public AppointmentDTO(int appointmentID, DateTime date, DateTime issueDate, TimeSpan start,  TimeSpan end ,int doctorID, int patientID, int appointmentStatusID , int serviceID)
        {
            AppointmentID = appointmentID;
            IssueDate = issueDate;
            this.Date = date;
            StartTime = start;
            EndTime = end;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentStatusID = appointmentStatusID;
            this.ServiceID = serviceID;
        }

        public AppointmentDTO(int appointmentID, DateTime date, DateTime issueDate, TimeSpan start, TimeSpan end, int doctorID, int patientID, int appointmentStatusID , int serivceID ,ServiceDTO Service)
        {
            AppointmentID = appointmentID;
            IssueDate = issueDate;
            StartTime = start;
            Date = date;
            EndTime = end;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentStatusID = appointmentStatusID;
            this.ServiceID = serivceID;
        }

    }
}
