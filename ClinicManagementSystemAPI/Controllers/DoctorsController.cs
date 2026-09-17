using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOsLayer;
using ClinicLogicLayer;
namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
<<<<<<< HEAD
        [HttpPost("AddNewDoctor" , Name = "AddNewDoctor")]
=======
        [HttpPost]
>>>>>>> 8f2f2d13f48c328b3ae837c3298a3edd51c2fdee
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
<<<<<<< HEAD

        [HttpGet("GetSpecializations", Name = "GetSpecializations")]
        public async Task<IActionResult> GetSepcializations()
        {
            Dictionary<string, int> specializations = await clsDoctor.GetSpecializations();
            if (specializations == null)
            {
                return BadRequest("Bad request from the client side");
            }
            return Ok(specializations);
        }
=======
>>>>>>> 8f2f2d13f48c328b3ae837c3298a3edd51c2fdee
    }
}
