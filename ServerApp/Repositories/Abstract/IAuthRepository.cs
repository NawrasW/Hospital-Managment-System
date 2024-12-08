
using ServerApp.Models.DTO;

namespace ServerApp.Repositories.Abstract
{
    public interface IAuthRepository
    {

        Task<DoctorDTO> LoginDoctor(UserForLoginDto loginDto);
        Task<PatientDTO> LoginPatient(UserForLoginDto loginDto);
        Task<bool> RegisterDoctor(DoctorDTO doctorDto);
        Task<bool> RegisterPatient(PatientDTO patientDto);
        Task<bool> UserAlreadyExists(string email);
    }
}