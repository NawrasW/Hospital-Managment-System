using ServerApp.Models.Domain;
using ServerApp.Models.DTO;

public interface IDepartmentRepository{



Task<List<DepartmentDTO>> GetAllDepartments();

 Task<DepartmentDTO> GetDepartmentById(int id);


Task<bool> AddUpdateDepartment(Department Department);


Task<bool> DeleteDepartment(int id);

}