using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;
using Healthcare_Data_Management_System.Models;

namespace Healthcare_Data_Management_System
{
    public class HealthcareContextFactory : IDesignTimeDbContextFactory<HealthcareContext>

    {
        public HealthcareContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthcareContext>();
            optionsBuilder.UseSqlServer("Server=LAWRENCEPC\\SQLEXPRESS;Database=HDMS_DB;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate = true");

            return new HealthcareContext(optionsBuilder.Options);
        }
    }
}
