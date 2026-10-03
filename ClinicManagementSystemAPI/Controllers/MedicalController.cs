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
            clsMedicalRecord mr = new clsMedicalRecord(record.Description , record.Diagnosis  , record.AdditionalNotes, record.DoctorID, record.PatientID, record.AppointmentID);

            bool isAdded = await mr.AddNewMedicalRecordAsync(record);
            if (isAdded == false)
            {
                return BadRequest(new { Success = false, Message = "Failed to add medical record." });
            }
            return Ok(new { Success = isAdded });
        }
    }
}
