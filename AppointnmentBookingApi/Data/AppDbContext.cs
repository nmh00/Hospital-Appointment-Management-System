﻿using Microsoft.EntityFrameworkCore;
using AppointmentBookingApi.Models;

namespace AppointmentBookingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
