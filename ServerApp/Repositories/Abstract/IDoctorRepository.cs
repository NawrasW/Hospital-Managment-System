using ServerApp.Models.Domain;
using ServerApp.Models.DTO;
namespace ServerApp.Repositories.Abstract
{
    public interface IDoctorRepository
    {

        Task<bool> AddUpdateDoctor(DoctorDTO Doctor);

        Task<List<Doctor>> GetAllDoctors();

        Task<bool> DeleteDoctor(int id);

        Task<Doctor> GetDoctorById(int id);

       // Task<UserLoginResponseDto> GetLoginDoctor(UserLoginReqDto user);
    }
}