public class ScheduleDTO
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string AvailableStartDay { get; set; }
    public string AvailableEndDay { get; set; }
    public DateTime AvailableStartTime { get; set; }
    public DateTime AvailableEndTime { get; set; }
    public string Status { get; set; }
}
