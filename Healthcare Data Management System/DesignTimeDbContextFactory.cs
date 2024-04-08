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
            optionsBuilder.UseSqlServer(Globals.ConnectionString);

            return new HealthcareContext(optionsBuilder.Options);
        }
    }
}
