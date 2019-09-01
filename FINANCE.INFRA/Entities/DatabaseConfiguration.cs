using Microsoft.Extensions.Configuration;

namespace FINANCE.INFRA.Entities
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private readonly string ConnectionString = "DBConnectionString";

        public string GetConnectionString()
        {
            return GetConfiguration().GetConnectionString(ConnectionString);
        }
    }
}
