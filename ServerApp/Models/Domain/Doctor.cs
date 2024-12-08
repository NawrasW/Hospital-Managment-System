using System;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Domain
{
    public class Doctor
    {
        public Doctor() 
        { 
            // Set default values if necessary
            CreatedAt = DateTime.UtcNow; 
            UpdatedAt = DateTime.UtcNow; 
        }

        public Doctor(int id, string? firstName, string? lastName, string? email, byte[] password, byte[] passwordKey,
                      UserRoles role, string phoneNumber, DateTime dateOfBirth, string gender, string address, 
                      string? imgName = null, int? departmentId = null, string? specialization = null, string? qualifications = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PasswordKey = passwordKey;
            Role = role;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            ImgName = imgName;
            DepartmentId = departmentId ?? 0;  // Default to 0 if null
            Specialization = specialization;
            Qualifications = qualifications;
            CreatedAt = DateTime.UtcNow; // Set created date
            UpdatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        // Encrypted password fields
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public UserRoles Role { get; set; } = UserRoles.Doctor; 


        public string? Token { get; set; }


        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Gender { get; set; }

        public string? ImgName { get; set; }

        [Required]
        public int DepartmentId { get; set; } // Assuming DepartmentId is a required field

        [Required]
        public string Specialization { get; set; } // Doctor's area of expertise

        [Required]
        public string Qualifications { get; set; } // Doctor's qualifications


         public ICollection<Schedule> Schedules { get; set; }
         
         public ICollection<Appointment> Appointments { get; set; }
    }
}
