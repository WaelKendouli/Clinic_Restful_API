using ClinicLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemAPI.Controllers
{

    [Route("api/ClinicServices")]
    [ApiController]
    public class ClinicServicesController : Controller
    {
       
        [HttpGet("GetAllSerivces", Name = "GetAllSerives")]
        public async Task <IActionResult> GetAllServices()
        {
            var list = await clsClinicService.GetClinicServicesAsync();
            if (list != null)
            {
                return Ok(list);
            }
            return BadRequest("There are no serivces stored in DB");
        }
    }
}
