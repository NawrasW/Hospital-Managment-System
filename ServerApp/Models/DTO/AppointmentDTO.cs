public class AppointmentDTO
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public string Problem { get; set; }
    public bool Status { get; set; }

    public string DoctorName { get; set; }

    public string PatientName { get; set; }
   
}
