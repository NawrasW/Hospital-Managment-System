using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
using ServerApp.Repositories.Abstract;

namespace ServerApp.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _context;

        public AuthRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DoctorDTO> LoginDoctor(UserForLoginDto loginDto)
        {
            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(d => d.Email == loginDto.Email);

            if (doctor == null || !VerifyPasswordHash(loginDto.Password, doctor.Password, doctor.PasswordKey))
                return null;

            return MapToDoctorDTO(doctor);
        }

        public async Task<PatientDTO> LoginPatient(UserForLoginDto loginDto)
        {
            var patient = await _context.Patient
                .FirstOrDefaultAsync(p => p.Email == loginDto.Email);

            if (patient == null || !VerifyPasswordHash(loginDto.Password, patient.Password, patient.PasswordKey))
                return null;

            return MapToPatientDTO(patient);
        }

        public async Task<bool> RegisterDoctor(DoctorDTO doctorDto)
        {
            if (await UserAlreadyExists(doctorDto.Email))
                return false;

            var doctorEntity = MapToDoctor(doctorDto);
            HashPassword(doctorDto.Password, out byte[] passwordKey, out byte[] passwordHash);
            doctorEntity.Password = passwordHash;
            doctorEntity.PasswordKey = passwordKey;
            doctorEntity.CreatedAt = DateTime.UtcNow;
            doctorEntity.UpdatedAt = DateTime.UtcNow;

            await _context.Doctor.AddAsync(doctorEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegisterPatient(PatientDTO patientDto)
        {
            if (await UserAlreadyExists(patientDto.Email))
                return false;

            var patientEntity = MapToPatient(patientDto);
            HashPassword(patientDto.Password, out byte[] passwordKey, out byte[] passwordHash);
            patientEntity.Password = passwordHash;
            patientEntity.PasswordKey = passwordKey;
            patientEntity.CreatedAt = DateTime.UtcNow;
            patientEntity.UpdatedAt = DateTime.UtcNow;

            await _context.Patient.AddAsync(patientEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserAlreadyExists(string email)
        {
            return await _context.Doctor.AnyAsync(d => d.Email == email) || 
                   await _context.Patient.AnyAsync(p => p.Email == email);
        }

        // Helper methods
        private void HashPassword(string password, out byte[] passwordKey, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedKey)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedKey))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        private Doctor MapToDoctor(DoctorDTO doctorDto)
        {
            return new Doctor
            {
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Email = doctorDto.Email,
                Role = UserRoles.Doctor,
                PhoneNumber = doctorDto.PhoneNumber,
                DateOfBirth = (DateTime)doctorDto.DateOfBirth,
                Gender = doctorDto.Gender,
                Address = doctorDto.Address,
              
              
                Specialization = doctorDto.Specialization,
                Qualifications = doctorDto.Qualifications
            };
        }

        private Patient MapToPatient(PatientDTO patientDto)
        {
            return new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Email = patientDto.Email,
                Role = UserRoles.Patient,
                PhoneNumber = patientDto.PhoneNumber,
                DateOfBirth = (DateTime)patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                Address = patientDto.Address
            };
        }

        private DoctorDTO MapToDoctorDTO(Doctor doctor)
        {
            return new DoctorDTO
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                DateOfBirth = doctor.DateOfBirth,
                Gender = doctor.Gender,
                Address = doctor.Address,
                 Role = doctor.Role.ToString(), 
                Specialization = doctor.Specialization,
                Qualifications = doctor.Qualifications
            };
        }

        private PatientDTO MapToPatientDTO(Patient patient)
        {
            return new PatientDTO
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address,
                 Role = patient.Role.ToString(), 
            };
        }
    }
}
