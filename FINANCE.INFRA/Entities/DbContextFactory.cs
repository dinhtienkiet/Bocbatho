using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FINANCE.INFRA.Entities
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FINANCEEntities>
    {
        private static string ConnectionString => new DatabaseConfiguration().GetConnectionString();
        public FINANCEEntities CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FINANCEEntities>();
            optionsBuilder.UseMySql(ConnectionString);
            return new FINANCEEntities(optionsBuilder.Options);
        }
    }
}
