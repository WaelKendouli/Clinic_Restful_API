using Microsoft.AspNetCore.Mvc;
using ClinicLogicLayer;
using DTOsLayer;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/Prescription")]
    [ApiController]
    public class PrescriptionController : Controller
    {
        [HttpPost("AddNewPrescription" , Name ="AddNewPrescription")]
        public async Task<IActionResult> AddNewPrescription([FromBody] PrescriptionDTO prescriptionDTO)
        {
            
            var result = await clsPrescription.AddNewPrescriptionAsync(prescriptionDTO);

            if (result)
            {
                return Ok("Prescription was added succesfully");
            }

            return BadRequest("Prescription was adding failed");
        }
    }
}
