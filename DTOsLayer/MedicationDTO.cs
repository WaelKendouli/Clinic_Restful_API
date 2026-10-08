using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class MedicationDTO
    {
        public int MedicationID { get; set; }
        public string MedicationName { get; set; }

        

        public MedicationDTO(int medicationID, string medicationName)
        {
            MedicationID = medicationID;
            MedicationName = medicationName;
        }
    }
}
