using DTOsLayer;
using DTOsLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace DataAccessLayer.Helpers
{
    public class clsRolesSelectingHandler
    {
      Dictionary<string, Func<UserDTO, Task<IF_User>>> _dRoleAndActions = new Dictionary<string, Func<UserDTO, Task<IF_User>>>();

        public clsRolesSelectingHandler()
        {
            _dRoleAndActions.Add("Patient", async  dto =>  await clsAuthenticationChecker.GetPatientDataByUserIDAsync(dto));
            _dRoleAndActions.Add("Doctor", async dto => await clsAuthenticationChecker.GetDoctorDataByUserIDAsync(dto));
            _dRoleAndActions.Add("Admin", async dto => await clsAuthenticationChecker.GetAdminDataByUserIDAsync(dto));

        }
        public async Task<IF_User> SelectRole(UserDTO DTO)
        {
            try
            {
                if (_dRoleAndActions.TryGetValue(DTO.Role, out var action))
                {
                    return await action(DTO);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }
    }
}
