using ServerApp.Models.Domain;
using ServerApp.Models.DTO;

public interface IAppointmentRepository
{
    Task<List<AppointmentDTO>> GetAllAppointments();

    Task<List<AppointmentDTO>> GetAppointmentsByDoctorIdAsync(int doctorId);

        // Method to get all appointments associated with a specific patient ID
    Task<List<AppointmentDTO>> GetAppointmentsByPatientIdAsync(int patientId);
    Task<AppointmentDTO> GetAppointmentById(int id);
    Task<bool> AddUpdateAppointment(AppointmentDTO appointment);
       
    Task<IEnumerable<DoctorDTO>> GetAvailableDoctorsAsync(DateTime appointmentDate);
    Task<bool> DeleteAppointment(int id);
}