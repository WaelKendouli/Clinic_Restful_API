using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class AppointmentItemDTO
    {
        public int AppointmentID { get; set; }
        public string FullName { get; set; }
        public string DoctorName { get; set; }
        public string AppointmentStatus { get; set; }
        public string Field { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public AppointmentItemDTO(int appointmentID, string fullName, string doctorName, string appointmentStatus, string field, DateTime date, TimeSpan time)
        {
            AppointmentID = appointmentID;
            FullName = fullName;
            DoctorName = doctorName;
            AppointmentStatus = appointmentStatus;
            Field = field;
            Date = date;
            Time = time;
        }
    }
}
