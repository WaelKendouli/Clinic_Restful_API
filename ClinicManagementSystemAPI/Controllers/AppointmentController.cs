using ClinicLogicLayer;
using DTOsLayer;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemAPI.Controllers
{

    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpPost("AddNewAppointment", Name = "AddNewAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAppointment([FromBody] AppointmentDTO DTO)
        {
            clsAppointment NewAppointment = new clsAppointment(DTO.Date, DTO.Time, DTO.DoctorID, DTO.PatientID, DTO.AppointmentStatusID);

            if (!await NewAppointment.AddNewAppointmentAsync(DTO.Date, DTO.Time, DTO.DoctorID, DTO.PatientID, DTO.AppointmentStatusID))
            {
                return BadRequest("New Appointment was not added");
            }
            return Ok(DTO);
        }

        [HttpGet("GetAppointmentList/{patientID}", Name = "GetAppointmentList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentList(int patientID)
        {
            List<AppointmentItemDTO> appointments = await clsAppointment.GetListAppointementByPatientIDAsync(patientID);
            if (appointments != null)
            {
                return Ok(appointments);
            }
            return NotFound("No appointments found for the specified patient.");
        }
    }
}
