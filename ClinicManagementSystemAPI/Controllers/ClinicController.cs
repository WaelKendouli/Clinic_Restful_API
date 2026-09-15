using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicLogicLayer;
using DTOsLayer;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        [HttpPost ("auth" ,Name = "AuthenticateUser")]
        public IActionResult AuthenticateUser([FromBody] LoginDTO DTO)
        { 
            try
            {
                LoggingAuthenticator authenticator = new LoggingAuthenticator(DTO.Username, DTO.Password, DTO.Phone, DTO.Email);
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
    }
}
