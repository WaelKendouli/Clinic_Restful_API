using Microsoft.AspNetCore.Mvc;
using ClinicLogicLayer;

namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : Controller
    {
        [HttpGet("GetAllPatients", Name = "GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await clsPatient.GetAllPatientsAsListAsync();
            if (patients == null)
            {
                return BadRequest("Bad request from the client side");
            }
            return Ok(patients);
        }
    }
}
