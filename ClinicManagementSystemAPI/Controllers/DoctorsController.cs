using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOsLayer;
using ClinicLogicLayer;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] DoctorDTO DTO)
        {
            clsDoctor NewDoctor = new clsDoctor(DTO.FirstName, DTO.LastName, DTO.DateOfBirth, DTO.Phone, DTO.Email,
                DTO.Address, DTO.Gender,
                DTO.PhotoURL, DTO.SpecializationID);
            if (!await NewDoctor.Save())
            {
                return BadRequest("New Doctor was not added");
            }
            return Ok(DTO);
        }
    }
}
