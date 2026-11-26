using Microsoft.AspNetCore.Mvc;
using DTOsLayer;
using ClinicLogicLayer;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/MedicalRecords")]
    [ApiController]
    public class MedicalController : Controller
    {
        [HttpPost("AddMedicalRecord" , Name = "AddMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecordDTO record)
        {
            return Ok();
        }
    }
}
