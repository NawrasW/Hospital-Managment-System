using ServerApp.Models.Domain;

public interface IScheduleRepository
{
   Task<List<Schedule>> GetAllSchedules();
    Task<ScheduleDTO> GetScheduleById(int id);
    Task<bool> AddUpdateSchedule(ScheduleDTO schedule);

    Task<bool> DeleteSchedule(int id);
}