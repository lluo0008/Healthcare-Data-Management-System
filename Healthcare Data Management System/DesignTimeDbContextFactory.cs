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
            optionsBuilder.UseSqlServer("Server=LAWRENCEPC\\SQLEXPRESS;Database=HDMS_DB;User Id =LAWRENCEPC\\xlllu;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate = true");

            return new HealthcareContext(optionsBuilder.Options);
        }
    }
}
