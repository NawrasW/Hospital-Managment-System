using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using ServerApp.Repositories.Abstract;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
namespace ServerApp.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public PatientRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

public async Task<bool> AddUpdatePatient(PatientDTO PatientDto)
{
    try
    {
        // Ensure the date properties are in UTC
        PatientDto.CreatedAt = DateTime.UtcNow; // For new entries
        PatientDto.UpdatedAt = DateTime.UtcNow; // Will be used in both add and update

        if (PatientDto.DateOfBirth.HasValue) // Check if DateOfBirth has a value
        {
            // Convert DateOfBirth to UTC if it's not already
            PatientDto.DateOfBirth = PatientDto.DateOfBirth.Value.ToUniversalTime();
        }

        var PatientEntity = _mapper.Map<Patient>(PatientDto);
        if (PatientDto.Id == 0)
        {
            // Adding a new Patient
            Console.WriteLine("Adding a new Patient...");

            // Hash the password before saving it
            if (!string.IsNullOrEmpty(PatientDto.Password))
            {
                HashPassword(PatientDto.Password, out byte[] passwordKey, out byte[] passwordHash);
                PatientEntity.PasswordKey = passwordKey;
                PatientEntity.Password = passwordHash;
            }

            await _context.Patient.AddAsync(PatientEntity);
        }
        else
        {
            // Updating existing Patient
            Console.WriteLine($"Updating Patient with ID: {PatientDto.Id}...");

            var existingPatient = await _context.Patient.FindAsync(PatientDto.Id);
            if (existingPatient == null)
            {
                Console.WriteLine($"Patient with ID {PatientDto.Id} not found.");
                return false;
            }

            // Update properties
            existingPatient.FirstName = PatientDto.FirstName;
            existingPatient.LastName = PatientDto.LastName;
            existingPatient.Email = PatientDto.Email;

            // Check if a new password is provided
            if (!string.IsNullOrEmpty(PatientDto.Password))
            {
                Console.WriteLine($"Updating password for Patient with ID: {PatientDto.Id}...");
                HashPassword(PatientDto.Password, out byte[] newPasswordKey, out byte[] newPasswordHash);
                existingPatient.PasswordKey = newPasswordKey;
                existingPatient.Password = newPasswordHash;
            }

            // Update the existing Patient
            _context.Patient.Update(existingPatient);
        }

        Console.WriteLine("Saving changes to the database...");
        var result = await _context.SaveChangesAsync();
        Console.WriteLine("Changes saved successfully.");

        return result > 0; // Returns true if operation was successful
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in AddUpdatePatient: {ex.Message}");
        return false;
    }
}


        public async Task<List<Patient>> GetAllPatients()
        {
            Console.WriteLine("Retrieving all Patients...");
            return await _context.Patient.ToListAsync();
        }

        public async Task<bool> DeletePatient(int id)
        {
            try
            {
                var Patient = await _context.Patient.FindAsync(id);
                if (Patient == null)
                {
                    Console.WriteLine($"Patient with ID {id} not found.");
                    return false;
                }

                Console.WriteLine($"Removing Patient with ID: {id}...");
                _context.Patient.Remove(Patient);
                var result = await _context.SaveChangesAsync();
                Console.WriteLine("Patient deleted successfully.");
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeletePatient: {ex.Message}");
                return false;
            }
        }

        public async Task<Patient> GetPatientById(int id)
        {
            Console.WriteLine($"Retrieving Patient with ID: {id}...");
            return await _context.Patient.FindAsync(id);
        }





          public async Task<UserLoginResponseDto> GetLoginUser(UserLoginReqDto user)
        {
            var result = await _context.Patient.FirstOrDefaultAsync(us => us.Email.ToLower() == user.Email );
            var userResult = new UserLoginResponseDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName

            };
            
            return userResult;
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
