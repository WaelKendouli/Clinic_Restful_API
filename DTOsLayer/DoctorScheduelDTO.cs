using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DoctorScheduelDTO
    {
      public TimeSpan StartTime { get; set; }
      public TimeSpan EndTime { get; set; }
      public int SlotDuration { get; set; }
      public bool IsActive { get; set; }

        public List<TimeSpan> liTimeSlots { get; set; } = new List<TimeSpan>();

        public DoctorScheduelDTO()
        {
            
        }

        private List<TimeSpan> _GenerateTimeSlots(TimeSpan start , TimeSpan end)
        {
            List<TimeSpan> TimeSlots = new List<TimeSpan>();
            TimeSpan time = start;
            while (time <= end)
            {
                TimeSlots.Add(time);
               time = time.Add(TimeSpan.FromMinutes(this.SlotDuration));
            }
            return TimeSlots;
        }

        public DoctorScheduelDTO(TimeSpan startTime, TimeSpan EndTime, int slotDuration, bool IsActive)
        {
            this.StartTime = startTime;
            this.EndTime = EndTime;
            this.SlotDuration = slotDuration;
            this.IsActive = IsActive;
            this.liTimeSlots = _GenerateTimeSlots(this.StartTime, this.EndTime);
        }

    }
}
