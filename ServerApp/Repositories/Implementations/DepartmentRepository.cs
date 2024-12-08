using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Domain;
using AutoMapper;
namespace ServerApp.Repositories.Implementations
{
public class DepartmentRepository : IDepartmentRepository
{


    private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public DepartmentRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    public async Task<bool> AddUpdateDepartment(Department department)
{
    if (department == null)
        throw new ArgumentNullException(nameof(department));

    var existingDepartment = await _context.Department
        .FirstOrDefaultAsync(d => d.Id == department.Id);

    if (existingDepartment == null)
    {
        // Create a new department if it doesn't exist
       
        _context.Department.Add(department);
    }
    else
    {
        // Update the existing department
        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description ?? existingDepartment.Description; // Retain existing description if null
        existingDepartment.Status = department.Status ?? existingDepartment.Status; // Retain existing status if null
    }

    try
    {
        await _context.SaveChangesAsync();
        return true;
    }
    catch (Exception ex)
    {
        // Log the exception (optional)
        Console.WriteLine($"Error saving department: {ex.Message}");
        return false;
    }
}



    public async Task<bool> DeleteDepartment(int id)
    {
        try
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
                return false;

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<DepartmentDTO>> GetAllDepartments()
    {
        try
        {
            var departments = await _context.Department.ToListAsync();
            return _mapper.Map<List<DepartmentDTO>>(departments);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<DepartmentDTO> GetDepartmentById(int id)
    {
        try
        {
            var department = await _context.Department.FindAsync(id);
            return department == null ? null : _mapper.Map<DepartmentDTO>(department);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
}