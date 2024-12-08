using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;

public class AppointmentRepository : IAppointmentRepository
{
        private readonly DatabaseContext _context;
      private readonly IMapper _mapper;

    public AppointmentRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context;
         _mapper = mapper;
    }
public async Task<bool> AddUpdateAppointment(AppointmentDTO appointmentDto)
{
    try
    {
        // Fetch the doctor's available schedule
        var schedule = await _context.Schedule
            .Where(s => s.DoctorId == appointmentDto.DoctorId &&
                        s.AvailableStartTime <= appointmentDto.AppointmentDate &&
                        s.AvailableEndTime >= appointmentDto.AppointmentDate)
            .FirstOrDefaultAsync();

        if (schedule == null)
        {
            Console.WriteLine("Selected time is outside the doctor's available schedule.");
            return false; // Appointment is not within available schedule
        }

        // Proceed with adding/updating the appointment if within schedule
        Appointment appointmentEntity = appointmentDto.Id == 0
            ? _mapper.Map<Appointment>(appointmentDto)
            : await _context.Appointment.FindAsync(appointmentDto.Id);

        if (appointmentEntity == null) return false;

        _mapper.Map(appointmentDto, appointmentEntity);
        _context.Appointment.Update(appointmentEntity);
        await _context.SaveChangesAsync();

        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in AddUpdateAppointment: {ex.Message}");
        return false;
    }
}



   

public async Task<List<AppointmentDTO>> GetAppointmentsByDoctorIdAsync(int doctorId)
{
    return await _context.Appointment
        .Where(a => a.DoctorId == doctorId)
        .Select(a => new AppointmentDTO
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            PatientId = a.PatientId,
            AppointmentDate = a.AppointmentDate,
            Problem = a.Problem,
            Status = a.Status,
            DoctorName = a.Doctor.FirstName,  // Assuming `Name` is the property holding the Doctor's name
            PatientName = a.Patient.FirstName // Assuming `Name` is the property holding the Patient's name
        }).ToListAsync();
}

public async Task<List<AppointmentDTO>> GetAppointmentsByPatientIdAsync(int patientId)
{
   return await _context.Appointment
        .Where(a => a.PatientId == patientId)
        .Select(a => new AppointmentDTO
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            PatientId = a.PatientId,
            AppointmentDate = a.AppointmentDate,
            Problem = a.Problem,
            Status = a.Status,
            DoctorName = a.Doctor.FirstName,  // Assuming `Name` is the property holding the Doctor's name
            PatientName = a.Patient.FirstName // Assuming `Name` is the property holding the Patient's name
        }).ToListAsync();
}

    

    public async Task<AppointmentDTO> GetAppointmentById(int id)
    {
        var appointment = await _context.Appointment.FindAsync(id);
        if (appointment == null) return null;

        return new AppointmentDTO
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            AppointmentDate = appointment.AppointmentDate,
            Problem = appointment.Problem,
            Status = appointment.Status
        };
    }

  public async Task<IEnumerable<DoctorDTO>> GetAvailableDoctorsAsync(DateTime appointmentDate)
    {
        // Retrieve available doctors based on the absence of scheduled appointments for the given date.
        var availableDoctors = await _context.Doctor
            .Include(d => d.Appointments)
            .Where(d => !d.Appointments.Any(a => a.AppointmentDate == appointmentDate))
            .ToListAsync();

        // Use AutoMapper to map from Doctor to DoctorDTO
        var doctorDTOs = _mapper.Map<IEnumerable<DoctorDTO>>(availableDoctors);

        return doctorDTOs;
    }
public async Task<List<AppointmentDTO>> GetAllAppointments()
{
    Console.WriteLine("Retrieving all Appointments...");
    
    return await _context.Appointment
        .Include(a => a.Doctor)
        .Include(a => a.Patient)
        .Select(a => new AppointmentDTO
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            PatientId = a.PatientId,
            AppointmentDate = a.AppointmentDate,
            Problem = a.Problem,
            Status = a.Status,
            DoctorName = a.Doctor != null ? a.Doctor.FirstName + " " + a.Doctor.LastName : null,
            PatientName = a.Patient != null ? a.Patient.FirstName + " " + a.Patient.LastName : null
        })
        .ToListAsync();
}


 

  public async Task<bool> DeleteAppointment(int id)
    {
        try
        {
            var Appointment = await _context.Appointment.FindAsync(id);
            if (Appointment == null)
            {
                Console.WriteLine($"Appointment with ID {id} not found.");
                return false;
            }

            Console.WriteLine($"Removing Appointment with ID: {id}...");
            _context.Appointment.Remove(Appointment);
            var result = await _context.SaveChangesAsync();
            Console.WriteLine("Appointment deleted successfully.");
            return result > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteAppointment: {ex.Message}");
            return false;
        }
    }

    
}
