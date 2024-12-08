using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using ServerApp.Repositories.Abstract;

namespace ServerApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [AllowAnonymous]
        [HttpGet("getAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctors();
            return Ok(doctors);
        }

        
        [AllowAnonymous]
        [HttpGet("getDoctorById/{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorRepository.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }
    [AllowAnonymous]
        [HttpPost("AddUpdateDoctor")]
        public async Task<IActionResult> AddUpdateDoctor([FromBody] DoctorDTO doctor)
        {
            var result = await _doctorRepository.AddUpdateDoctor(doctor);
            if (result)
            {
                return Ok("Doctor saved successfully.");
            }
            return BadRequest("Failed to save doctor.");
        }
   [AllowAnonymous]
        [HttpDelete("deleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorRepository.DeleteDoctor(id);
            if (result)
            {
                return Ok("Doctor deleted successfully.");
            }
            return BadRequest("Failed to delete doctor.");
        }
    }
}
