using Microsoft.AspNetCore.Mvc;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using ServerApp.Repositories.Abstract;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _PatientRepository;

        public PatientController(IPatientRepository PatientRepository)
        {
            _PatientRepository = PatientRepository;
        }

        [HttpGet("getAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var Patients = await _PatientRepository.GetAllPatients();
            return Ok(Patients);
        }

        [HttpGet("getPatientById/{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var Patient = await _PatientRepository.GetPatientById(id);
            if (Patient == null)
            {
                return NotFound();
            }
            return Ok(Patient);
        }

        [HttpPost("AddUpdatePatient")]
        public async Task<IActionResult> AddUpdatePatient([FromBody] PatientDTO Patient)
        {
            var result = await _PatientRepository.AddUpdatePatient(Patient);
            if (result)
            {
                return Ok("Patient saved successfully.");
            }
            return BadRequest("Failed to save Patient.");
        }

        [HttpDelete("deletePatient/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _PatientRepository.DeletePatient(id);
            if (result)
            {
                return Ok("Patient deleted successfully.");
            }
            return BadRequest("Failed to delete Patient.");
        }




        [HttpPost("getLoginUser")]
        public async Task<UserLoginResponseDto> GetLoginUser (UserLoginReqDto user)
        {
            var result = await _PatientRepository.GetLoginUser(user);

            return result;

        }
    }
}
