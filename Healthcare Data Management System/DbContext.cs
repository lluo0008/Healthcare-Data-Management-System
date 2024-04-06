using Healthcare_Data_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_Data_Management_System
{
    public class HealthcareContext : DbContext
    {
        public HealthcareContext(DbContextOptions<HealthcareContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
    }
}
