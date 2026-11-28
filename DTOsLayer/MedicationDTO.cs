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
        public int CategoryID { get; set; }
        public int FormID { get; set; }
        public string Strength { get; set; }
        public string Instructions { get; set; }

        public MedicationDTO()
        {
            MedicationName = string.Empty;
            Strength = string.Empty;
            Instructions = string.Empty;
        }

        public MedicationDTO(int medicationID, string medicationName, int categoryID, int formID, string strength, string instructions)
        {
            MedicationID = medicationID;
            MedicationName = medicationName;
            CategoryID = categoryID;
            FormID = formID;
            Strength = strength;
            Instructions = instructions;
        }
    }
}
