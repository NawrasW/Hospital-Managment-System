using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using ServerApp.Repositories.Abstract;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
namespace ServerApp.Repositories.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public DoctorRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

public async Task<bool> AddUpdateDoctor(DoctorDTO doctorDto)
{
    try
    {
        Console.WriteLine("Entering AddUpdateDoctor method...");

        // Validate and fetch the department
        Department department = null;
        if (doctorDto.DepartmentId.HasValue)
        {
            department = await _context.Department.FindAsync(doctorDto.DepartmentId.Value);
            if (department == null)
            {
                Console.WriteLine($"Department with ID {doctorDto.DepartmentId} not found.");
                return false;
            }
        }

        Doctor doctorEntity;

        if (doctorDto.Id == 0)
        {
            Console.WriteLine("Adding a new doctor...");
            // Add new doctor
            doctorEntity = new Doctor
            {
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                DateOfBirth = doctorDto.DateOfBirth.Value.ToUniversalTime(), // Nullable handling
                Address = doctorDto.Address,
                Specialization = doctorDto.Specialization,
                Qualifications = doctorDto.Qualifications,
                Gender = doctorDto.Gender,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = UserRoles.Doctor, // Default role
                DepartmentId = department?.Id ?? 0 // Use department ID if valid, else default
            };

            // Hash password if provided
            if (!string.IsNullOrEmpty(doctorDto.Password))
            {
                HashPassword(doctorDto.Password, out byte[] passwordKey, out byte[] passwordHash);
                doctorEntity.PasswordKey = passwordKey;
                doctorEntity.Password = passwordHash;
            }

            await _context.Doctor.AddAsync(doctorEntity);
        }
        else
        {
            Console.WriteLine($"Updating doctor with ID: {doctorDto.Id}...");
            // Update existing doctor
            doctorEntity = await _context.Doctor.FindAsync(doctorDto.Id);
            if (doctorEntity == null)
            {
                Console.WriteLine($"Doctor with ID {doctorDto.Id} not found.");
                return false;
            }

            // Update properties
            doctorEntity.FirstName = doctorDto.FirstName;
            doctorEntity.LastName = doctorDto.LastName;
            doctorEntity.Email = doctorDto.Email;
            doctorEntity.PhoneNumber = doctorDto.PhoneNumber;
            doctorEntity.DateOfBirth = doctorDto.DateOfBirth.Value.ToUniversalTime(); // Nullable handling
            doctorEntity.Address = doctorDto.Address;
            doctorEntity.Specialization = doctorDto.Specialization;
            doctorEntity.Qualifications = doctorDto.Qualifications;
            doctorEntity.Gender = doctorDto.Gender;

            if (!string.IsNullOrEmpty(doctorDto.Role))
            {
                doctorEntity.Role = doctorDto.Role switch
                {
                    "Admin" => UserRoles.Admin,
                    "Doctor" => UserRoles.Doctor,
                    "Patient" => UserRoles.Patient,
                    _ => doctorEntity.Role // Keep existing role if invalid
                };
            }

            // Update department if provided
            if (doctorDto.DepartmentId.HasValue)
            {
                doctorEntity.DepartmentId = department?.Id ?? doctorEntity.DepartmentId;
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(doctorDto.Password))
            {
                Console.WriteLine($"Updating password for doctor with ID: {doctorDto.Id}...");
                HashPassword(doctorDto.Password, out byte[] newPasswordKey, out byte[] newPasswordHash);
                doctorEntity.PasswordKey = newPasswordKey;
                doctorEntity.Password = newPasswordHash;
            }

            _context.Doctor.Update(doctorEntity);
        }

        // Save changes to the database
        Console.WriteLine("Saving changes to the database...");
        await _context.SaveChangesAsync();
        Console.WriteLine("Changes saved successfully.");
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in AddUpdateDoctor: {ex.Message}");
        return false;
    }
}





        public async Task<List<Doctor>> GetAllDoctors()
        {
            Console.WriteLine("Retrieving all doctors...");
            return await _context.Doctor.ToListAsync();
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            try
            {
                var doctor = await _context.Doctor.FindAsync(id);
                if (doctor == null)
                {
                    Console.WriteLine($"Doctor with ID {id} not found.");
                    return false;
                }

                Console.WriteLine($"Removing doctor with ID: {id}...");
                _context.Doctor.Remove(doctor);
                var result = await _context.SaveChangesAsync();
                Console.WriteLine("Doctor deleted successfully.");
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteDoctor: {ex.Message}");
                return false;
            }
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            Console.WriteLine($"Retrieving doctor with ID: {id}...");
            return await _context.Doctor.FindAsync(id);
        }

        private void HashPassword(string password, out byte[] passwordKey, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

      
    }
}
