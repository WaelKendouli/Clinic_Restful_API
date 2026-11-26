using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class VitalsDTO
    {
        public int VitalsID { get; set; }
        public int MedicalRecordID { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodPressure { get; set; }
        public decimal Temperature { get; set; }
        public int Pulse { get; set; }

        // Parameterless constructor
        public VitalsDTO()
        {
            BloodPressure = string.Empty;
        }

        // Parameterized constructor
        public VitalsDTO(int vitalsID, int medicalRecordID, decimal height, decimal weight,
                         string bloodPressure, decimal temperature, int pulse)
        {
            VitalsID = vitalsID;
            MedicalRecordID = medicalRecordID;
            Height = height;
            Weight = weight;
            BloodPressure = bloodPressure;
            Temperature = temperature;
            Pulse = pulse;
        }
    }
}
