using ClinicManagementSystemAPI.Interfaces;
using ClinicManagementSystemAPI.Utilities;
using DTOsLayer;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/EmailService")]
    [ApiController]
    public class EmailSerivceController : ControllerBase
    {
        private readonly ISendEmail _emailSender;
        public EmailSerivceController(ISendEmail emailSender) => _emailSender = emailSender;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("SendEmail" , Name ="SendEmail")]
        public async Task<IActionResult> SendAnEmail([FromBody] EmailSenderDTO DTO)
        {
            if (string.IsNullOrEmpty(DTO.to) || string.IsNullOrEmpty(DTO.from) ||
                string.IsNullOrEmpty(DTO.subject) || string.IsNullOrEmpty(DTO.Body))
            {
                return BadRequest("some filed are invalid");
            }
            try
            {
                await _emailSender.SendAsync(DTO);
                return Ok(new { status = "submitted", message = "Email accepted by SMTP server" });
            }
            catch (SmtpCommandException ex) when (ex.ErrorCode == SmtpErrorCode.MessageNotAccepted)
            {
               
                return BadRequest($"Recipient rejected: {ex.Message}");
            }
            catch (SmtpCommandException ex)
            {
                
                return StatusCode(500, $"SMTP error: {ex.StatusCode} - {ex.Message}");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Send failed: {ex.Message}");
            }
        }
    }
}
