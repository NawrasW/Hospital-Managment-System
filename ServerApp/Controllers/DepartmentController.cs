using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ServerApp.Models.Domain;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentrepository;

        public DepartmentController(IDepartmentRepository departmentrepository)
        {
            _departmentrepository = departmentrepository;
        }

        // Add or Update a Department
        [HttpPost("AddUpdateDepartment")]
        public async Task<IActionResult> AddUpdateDepartment([FromBody] Department department)
        {
            if (department == null)
                return BadRequest("Department data is required.");

            var result = await _departmentrepository.AddUpdateDepartment(department);

            if (result)
                return Ok("Department saved successfully.");
            else
                return StatusCode(500, "Failed to save the department.");
        }

        // Delete a Department
        [HttpDelete("deleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentrepository.DeleteDepartment(id);

            if (result)
                return Ok("Department deleted successfully.");
            else
                return NotFound("Department not found.");
        }

        // Get all Departments
        [HttpGet("getAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentrepository.GetAllDepartments();

            if (departments == null || departments.Count == 0)
                return NotFound("No departments found.");

            return Ok(departments);
        }

        // Get a Department by ID
        [HttpGet("getDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentrepository.GetDepartmentById(id);

            if (department == null)
                return NotFound("Department not found.");

            return Ok(department);
        }
    }
}
