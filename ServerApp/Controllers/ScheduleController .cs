using Microsoft.AspNetCore.Mvc;
using ServerApp.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleController(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    // GET: api/Schedule/getAllSchedules
    [HttpGet("getAllSchedules")]
    public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetAllSchedules()
    {
        var schedules = await _scheduleRepository.GetAllSchedules();
        return Ok(schedules);
    }

    // GET: api/Schedule/getScheduleById/5
    [HttpGet("getScheduleById/{id}")]
    public async Task<ActionResult<ScheduleDTO>> GetSchedule(int id)
    {
        var schedule = await _scheduleRepository.GetScheduleById(id);
        if (schedule == null)
        {
            return NotFound($"Schedule with ID {id} not found.");
        }
        return Ok(schedule);
    }

    // POST: api/Schedule/AddUpdateSchedule
    [HttpPost("AddUpdateSchedule")]
    public async Task<IActionResult> AddOrUpdateSchedule([FromBody] ScheduleDTO scheduleDto)
    {
        if (scheduleDto == null)
        {
            return BadRequest("Schedule data is missing.");
        }

        var success = await _scheduleRepository.AddUpdateSchedule(scheduleDto);
        if (success)
        {
            return Ok(new { Message = "Schedule added or updated successfully." });
        }
        return StatusCode(500, "An error occurred while saving the schedule.");
    }

    // DELETE: api/Schedule/deleteSchedule/5
    [HttpDelete("deleteSchedule/{id}")]
    public async Task<IActionResult> DeleteSchedule(int id)
    {
        var success = await _scheduleRepository.DeleteSchedule(id);
        if (success)
        {
            return NoContent();
        }
        return NotFound($"Schedule with ID {id} not found.");
    }
}
