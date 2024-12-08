using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Domain;
using AutoMapper;

public class ScheduleRepository : IScheduleRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public ScheduleRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> AddUpdateSchedule(ScheduleDTO scheduleDto)
{
    try
    {
        Schedule scheduleEntity;

        if (scheduleDto.Id == 0)
        {
            // Check for existing schedule by DoctorId
            scheduleEntity = await _context.Schedule
                .FirstOrDefaultAsync(s => s.DoctorId == scheduleDto.DoctorId);

            if (scheduleEntity != null)
            {
                // Update the existing schedule (without modifying the Id)
                Console.WriteLine($"Updating existing schedule for DoctorId: {scheduleDto.DoctorId}...");
                scheduleEntity.AvailableStartDay = scheduleDto.AvailableStartDay;
                scheduleEntity.AvailableEndDay = scheduleDto.AvailableEndDay;
                scheduleEntity.AvailableStartTime = scheduleDto.AvailableStartTime;
                scheduleEntity.AvailableEndTime = scheduleDto.AvailableEndTime;
                scheduleEntity.Status = scheduleDto.Status;

                _context.Schedule.Update(scheduleEntity);
            }
            else
            {
                // Add a new schedule
                Console.WriteLine("Adding a new schedule...");
                scheduleEntity = _mapper.Map<Schedule>(scheduleDto);
                await _context.Schedule.AddAsync(scheduleEntity);
            }
        }
        else
        {
            // Updating a specific schedule by Id
            Console.WriteLine($"Updating schedule with ID: {scheduleDto.Id}...");
            scheduleEntity = await _context.Schedule.FindAsync(scheduleDto.Id);

            if (scheduleEntity == null)
            {
                Console.WriteLine($"Schedule with ID {scheduleDto.Id} not found.");
                return false;
            }

            // Update fields manually
            scheduleEntity.AvailableStartDay = scheduleDto.AvailableStartDay;
            scheduleEntity.AvailableEndDay = scheduleDto.AvailableEndDay;
            scheduleEntity.AvailableStartTime = scheduleDto.AvailableStartTime;
            scheduleEntity.AvailableEndTime = scheduleDto.AvailableEndTime;
            scheduleEntity.Status = scheduleDto.Status;

            _context.Schedule.Update(scheduleEntity);
        }

        await _context.SaveChangesAsync();
        Console.WriteLine("Changes saved successfully.");
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in AddUpdateSchedule: {ex.Message}");
        return false;
    }
}



    public async Task<ScheduleDTO> GetScheduleById(int id)
    {
        Console.WriteLine($"Retrieving schedule with ID: {id}...");
        var schedule = await _context.Schedule.FindAsync(id);
        
        if (schedule == null) return null;

        return _mapper.Map<ScheduleDTO>(schedule);
    }

    public async Task<List<Schedule>>  GetAllSchedules()
    {
        Console.WriteLine("Retrieving all schedules...");
        
       
         return await _context.Schedule.ToListAsync();
    }

    public async Task<bool> DeleteSchedule(int id)
    {
        try
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                Console.WriteLine($"Schedule with ID {id} not found.");
                return false;
            }

            Console.WriteLine($"Removing schedule with ID: {id}...");
            _context.Schedule.Remove(schedule);
            var result = await _context.SaveChangesAsync();
            Console.WriteLine("Schedule deleted successfully.");
            return result > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteSchedule: {ex.Message}");
            return false;
        }
    }


}
