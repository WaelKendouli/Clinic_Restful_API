using DTOsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace ClinicLogicLayer
{
    public class clsClinicService
    {
        public static async Task<List<ServiceDTO>> GetClinicServicesAsync()
        {
            return await clsClinicServicesDA.GetClinicServicesAsync();
        }

    }
}
