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
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentStatusID { get; set; }

        public AppointmentDTO(int appointmentID, DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
            AppointmentID = appointmentID;
            Date = date;
            Time = time;
            DoctorID = doctorID;
            PatientID = patientID;
            AppointmentStatusID = appointmentStatusID;
        }


    }
}
