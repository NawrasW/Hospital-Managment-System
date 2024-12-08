using ServerApp.Models.Domain;
using ServerApp.Models.DTO;

namespace ServerApp.Repositories.Abstract
{
    public interface IPatientRepository
    {

        Task<bool> AddUpdatePatient(PatientDTO patient);

        Task<List<Patient>> GetAllPatients();

        Task<bool> DeletePatient(int id);

        Task<Patient> GetPatientById(int id);

        
         Task<UserLoginResponseDto> GetLoginUser(UserLoginReqDto user);

     
    }
}