namespace ServerApp.Models.DTO
{
    public class PatientDTO
    {
      

        public int Id { get; set; }
        
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }   

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? BloodType { get; set; }
        
      public string? Role { get; set; }


        public string? Token { get; set; }

        public List<string>? Allergies { get; set; }
        
        public string? InsuranceDetails { get; set; }

        public DateTime? CreatedAt { get; set; }

        
        public DateTime? UpdatedAt { get; set; }

    }
}