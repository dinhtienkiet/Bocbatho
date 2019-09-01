using FINANCE.INFRA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.INFRA.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        FINANCEEntities Init();
    }
}
