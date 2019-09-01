using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.INFRA.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
