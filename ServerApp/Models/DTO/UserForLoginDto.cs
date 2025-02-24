﻿namespace ServerApp.Models.DTO
{
    public class UserForLoginDto
    {
       
       public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class UserLoginDto
    {

         public int Id { get; set; }
       
      public string Email { get; set; } = string.Empty;
      
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;


        public string Token { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}