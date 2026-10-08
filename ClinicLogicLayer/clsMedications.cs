using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicLogicLayer
{
    public class clsMedications
    {
        public async Task<List<DTOsLayer.MedicationDTO>> GetListOfMedicationsAsync()
        {
            return await DataAccessLayer.MedicationsDA.GetListOfMedicationsAsync();
        }
    }
}
