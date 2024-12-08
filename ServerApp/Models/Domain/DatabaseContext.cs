using Microsoft.EntityFrameworkCore;
﻿using Microsoft.AspNetCore.Identity;
namespace ServerApp.Models.Domain

{

 public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<Medicine> Medicine { get; set; }











    }

}