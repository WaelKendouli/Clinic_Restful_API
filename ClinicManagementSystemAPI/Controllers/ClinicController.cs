using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicLogicLayer;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        [HttpPost ("auth" ,Name = "AuthenticateUser")]
        public IActionResult AuthenticateUser(string username, string password, string phone = "", string email = "")
        { 
            try
            {
                LoggingAuthenticator authenticator = new LoggingAuthenticator(username, password, phone, email);
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
        public IActionResult AuthenticateUserSecondaryMethod( string phone , string email )
        {
            try
            {
                LoggingAuthenticator authenticator = new LoggingAuthenticator("", "", phone, email);
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
