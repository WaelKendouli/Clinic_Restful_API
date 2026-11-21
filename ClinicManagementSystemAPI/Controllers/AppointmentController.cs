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

            if (!await clsAppointment.AddNewAppointmentAsync(DTO))
            {
                return BadRequest("New Appointment was not added");
            }
            return Ok(DTO);
        }

        [HttpGet("GetAppointmentList/{patientID}", Name = "GetAppointmentList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentListPerPatient(int patientID)
        {
            List<us_AppointmentItemDTO> appointments = await clsAppointment.GetListAppointementByPatientIDAsync(patientID);
            if (appointments != null)
            {
                return Ok(appointments);
            }
            return NotFound("No appointments found for the specified patient.");
        }

        [HttpPut("CancelAppointment/{appointmentID}", Name = "CancelAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelAppointment(int appointmentID)
        {
            AppointmentDTO Appointment = await clsAppointment.FindAppointmentByIDAsync(appointmentID);
            if (Appointment.IssueDate > DateTime.Today)
            {
                return BadRequest("Failed to cancel the appointment , appointments can be cancel only within 24 hours of thier ");
            }

            if (!await clsAppointment.ChangeAppointmentStatusToCanceledAsync(appointmentID))
            {
                return BadRequest("Failed to cancel the appointment.");
            }
            return Ok("Appointment canceled successfully.");
        }
        [HttpPut("UpdateAppointment/{appointmentID}", Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAppointment(int appointmentID, [FromBody] AppointmentDTO DTO)
        {
            if (!await clsAppointment.UpdateAppointmentAsync(appointmentID, DTO.IssueDate, DTO.StartTime, DTO.DoctorID, DTO.PatientID, DTO.AppointmentStatusID))
            {
                return BadRequest("Failed to update the appointment.");
            }
            return Ok("Appointment updated successfully.");
        }

        [HttpGet("GetCancelledAppointmentList/{patientID}", Name = "GetCancelledAppointmentList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCancelledAppointmentsListPerPatient(int patientID)
        {
            List<us_AppointmentItemDTO> CancelledAppointments = await clsAppointment.GetListOfCancelledAppointmentsAsync(patientID);
            if (CancelledAppointments != null)
            {
                return Ok(CancelledAppointments);
            }
            return NotFound("No appointments found for the specified patient.");
        }

    }
}
