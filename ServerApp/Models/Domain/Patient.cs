using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Domain
{
    public class Patient
    {
        public Patient() 
        { 
            CreatedAt = DateTime.Now; 
            UpdatedAt = DateTime.Now; 
            Allergies = new List<string>();
        }

        public Patient(int id, string? firstName, string? lastName, string? email, byte[] password, byte[] passwordKey,
                       UserRoles role, string phoneNumber, DateTime dateOfBirth, string gender, string address,
                       string? imgName = null, string? bloodType = null, string insuranceDetails = null, List<string> allergies = null)
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
            BloodType = bloodType;
            InsuranceDetails = insuranceDetails;
            Allergies = allergies ?? new List<string>(); // Default to an empty list if null
            CreatedAt = DateTime.Now; // Set created date
            UpdatedAt = DateTime.Now; // Set updated date
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
        public UserRoles Role { get; set; } = UserRoles.Patient; 


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
        public string? BloodType { get; set; } // Blood type of the patient

        [Required]
        public List<string> Allergies { get; set; } // Known allergies

        [Required]
        public string InsuranceDetails { get; set; } // Patient's insurance details


         public ICollection<Appointment> Appointments { get; set; }
    }
}
