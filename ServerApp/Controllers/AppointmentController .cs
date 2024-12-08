using Microsoft.AspNetCore.Mvc;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    // GET: api/Appointment/getAllAppointments
    [HttpGet("getAllAppointments")]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointments()
    {
        var appointments = await _appointmentRepository.GetAllAppointments();
        return Ok(appointments);
    }

    // GET: api/Appointment/getAppointmentById/5
    [HttpGet("getAppointmentById/{id}")]
    public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
    {
        var appointment = await _appointmentRepository.GetAppointmentById(id);
        if (appointment == null)
        {
            return NotFound($"Appointment with ID {id} not found.");
        }
        return Ok(appointment);
    }

 // GET: api/Appointment/getAppointmentsByUser/5
 [HttpGet("getAppointmentsByUser/{role}/{userId}")]
public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentsByUser(string role, int userId)
{
    try
    {
        List<AppointmentDTO> appointments;
        
        if (role == "Doctor")
        {
            appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(userId);
        }
        else if (role == "Patient")
        {
            appointments = await _appointmentRepository.GetAppointmentsByPatientIdAsync(userId);
        }
        else
        {
            return BadRequest("Invalid user role.");
        }

        // Log appointments to confirm data
        Console.WriteLine("Fetched appointments:", appointments.Count > 0 ? appointments : "No appointments found.");

        return Ok(appointments);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in fetching appointments: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}


[HttpGet("getAvailableDoctors")]
public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAvailableDoctors(DateTime appointmentDate)
{
    try
    {
        var availableDoctors = await _appointmentRepository.GetAvailableDoctorsAsync(appointmentDate);

        if (availableDoctors == null || !availableDoctors.Any())
        {
            return NotFound("No available doctors found for the specified date.");
        }

        return Ok(availableDoctors);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in fetching available doctors: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}

    // POST: api/Appointment/AddUpdateAppointment
    [HttpPost("AddUpdateAppointment")]
    public async Task<IActionResult> AddUpdateAppointment([FromBody] AppointmentDTO appointmentDto)
    {
        if (appointmentDto == null)
        {
            return BadRequest("Appointment data is missing.");
        }

        var success = await _appointmentRepository.AddUpdateAppointment(appointmentDto);
        if (success)
        {
            return Ok(new { Message = "Appointment added or updated successfully." });
        }
        return StatusCode(500, "An error occurred while saving the appointment.");
    }

    // DELETE: api/Appointment/deleteAppointment/5
    [HttpDelete("deleteAppointment/{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var success = await _appointmentRepository.DeleteAppointment(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound($"Appointment with ID {id} not found.");
    }
}
