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

        public DoctorScheduelDTO()
        {
            
        }
        public DoctorScheduelDTO(TimeSpan startTime, TimeSpan EndTime, int slotDuration, bool IsActive)
        {
            this.StartTime = startTime;
            this.EndTime = EndTime;
            this.SlotDuration = slotDuration;
            this.IsActive = IsActive;
        }

    }
}
