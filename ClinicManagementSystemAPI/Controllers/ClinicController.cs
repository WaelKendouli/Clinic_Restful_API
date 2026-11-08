using ClinicLogicLayer;
using ClinicManagementSystemAPI.Interfaces;
using DTOsLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/Clinic")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly ISendEmail _emailSender;
        public ClinicController(ISendEmail emailSender) => _emailSender = emailSender;

        [HttpPost ("auth" ,Name = "AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginTestClassDTO DTO)
        { 
            try
            {
                LoggingAuthenticator authenticator = new LoggingAuthenticator(DTO.Username, DTO.Password);
                
                if (authenticator.Authenticate())
                {
                    return Ok(authenticator.UserDTO);
                }
                else
                {
                    return Unauthorized(new { message = "Authentication failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("TestingSendingEmail", Name = "Test send email")]
        public async Task<IActionResult> SendEmailForTesting([FromBody] string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("Email is required");
            }
            await _emailSender.SendAsync(Email, "Test from your API clinic",
             "<p>Hello 👋 wael this message is from your API</p>");
            return Ok("message sent successfully");
        }
        /*
        [HttpPost("Sec-auth" ,Name = "AuthenticateUserWithSecondaryMethod")]
        public IActionResult AuthenticateUserSecondaryMethod([FromBody] LoginDTO DTO)
        {
            try
            {
                LoggingAuthenticator authenticator = new LoggingAuthenticator(DTO.Username, DTO.Password, DTO.Phone, DTO.Email);
                authenticator.SwitchToSecondaryAuthenticationMethod();
                bool isAuthenticated = authenticator.Authenticate();
                if (isAuthenticated)
                {
                    return Ok(new { message = "Authentication successful" });
                }
                else
                {
                    return Unauthorized(new { message = "Authentication failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        */
    }
}
