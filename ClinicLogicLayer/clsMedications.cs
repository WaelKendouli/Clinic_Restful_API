using DTOsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicLogicLayer
{
    public class clsMedications
    {
        public static async Task<List<MedicationDTO>> GetListOfMedicationsAsync()
        {
            return await DataAccessLayer.MedicationsDA.GetListOfMedicationsAsync();
        }

        public static async Task<Dictionary<string, int>> GetMedicationCategoriesAsync()
        {
            return await DataAccessLayer.MedicationsDA.GetMedicationCategoriesAsync();
        }
        public static async Task<List<DosageFormDTO>> GetListOfDosageFormsAsync()
        {
            return await DataAccessLayer.MedicationsDA.GetListOfDosageFormsAsync();
        }
    }
}
