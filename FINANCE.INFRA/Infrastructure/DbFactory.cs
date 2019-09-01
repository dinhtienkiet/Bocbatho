using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.INFRA.Entities;

namespace FINANCE.INFRA.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        FINANCEEntities dbContext;

        public FINANCEEntities Init()
        {
            return dbContext ?? (dbContext = new DesignTimeDbContextFactory().CreateDbContext(null));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
