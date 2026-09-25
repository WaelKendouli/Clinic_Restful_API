using Microsoft.AspNetCore.Mvc;
using ClinicLogicLayer;
using DTOsLayer;
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

        [HttpPost("AddNewPatient" , Name = "AddNewPatient")]
        public async Task<IActionResult> AddNewPatient([FromBody] PatientDTO patient)
        {
            var NewPatient = new clsPatient(patient.FirstName, patient.LastName ,
                patient.DateOfBirth , patient.Email , patient.Address ,
                patient.Phone , patient.Gender , patient.Password);

            if ( await NewPatient.AddNewPatient() == -1)
            {
                return BadRequest("Failed to add new patient");
            }

            return Ok(new { NewID = NewPatient.PatientID });
        }
    }
}
