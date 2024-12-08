using ServerApp.Models.Domain;

namespace ServerApp.Models.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }   

        public string? Gender { get; set; }


        public int? DepartmentId { get; set; } 

        public string? Address { get; set; }

        public string? Specialization { get; set; }
        

        public string? Qualifications { get; set; }


       public string? Role { get; set; }

        public string? Token { get; set; }
      

        public DateTime? CreatedAt { get; set; }

        
        public DateTime? UpdatedAt { get; set; }

    }
}