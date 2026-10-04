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
            var prescription = new clsPrescription(
                medicalRecordID: prescriptionDTO.MedicalRecordID,
                startDate: prescriptionDTO.StartDate,
                endDate: prescriptionDTO.EndDate
            );

            var result = await prescription.AddNewPrescriptionAsync();

            if (result)
            {
                return Ok(new { prescription, Success = true });
            }

            return BadRequest(new { Success = false });
        }
    }
}
