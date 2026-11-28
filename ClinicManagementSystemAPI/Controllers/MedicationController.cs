using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemAPI.Controllers
{
    [Route("api/Medications")]
    [ApiController]
    public class MedicationController : Controller
    {
        [HttpGet("GetListOfMedications", Name = "GetListOfMedications")]
        public async Task<IActionResult> GetListOfMedications()
        {
            var medications = await  ClinicLogicLayer.clsMedications.GetListOfMedicationsAsync();
            if (medications == null)
            {
                return BadRequest("Failed to retrieve list of Medications.");
            }
            return Ok(medications);
        }

        [HttpGet("GetListOfMedicationCategories", Name = "GetListOfMedicationCategories")]
        public async Task<IActionResult> GetListOfCategories()
        {
            var Categories = await ClinicLogicLayer.clsMedications.GetMedicationCategoriesAsync();
            if (Categories == null)
            {
                return BadRequest("Failed to retrieve list of Categories.");
            }
            return Ok(Categories);
        }

        [HttpGet("GetListOfDosageForm", Name = "GetListOfDosageForms")]
        public async Task<IActionResult> GetListOfDosageForms()
        {
            var list = await ClinicLogicLayer.clsMedications.GetListOfDosageFormsAsync();
            if (list == null)
            {
                return BadRequest("Failed to retrieve list of Categories.");
            }
            return Ok(list);
        }
    }
}
