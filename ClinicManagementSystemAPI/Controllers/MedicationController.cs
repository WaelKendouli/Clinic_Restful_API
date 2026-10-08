using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/Medications")]
    [ApiController]
    public class MedicationController : Controller
    {
        [HttpGet("GetListOfMedications", Name = "GetListOfMedications")]
        public async Task<IActionResult> GetListOfMedications()
        {
            var medications = await new ClinicLogicLayer.clsMedications().GetListOfMedicationsAsync();
            if (medications == null)
            {
                return BadRequest("Failed to retrieve list of medications.");
            }
            return Ok(medications);
        }
    }
}
