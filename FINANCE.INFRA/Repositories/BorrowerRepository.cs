

using System.Collections.Generic;
using System.Linq;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class BorrowerRepository : RepositoryBase<Borrower>, IUBorrowerRepository
    {
        public BorrowerRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Borrower Delete(Borrower borrower)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var borrower1 = DbContext.InstallmentLoanContracts.Where(t => t.BorrowerID == borrower.BorrowerID);
                    DbContext.InstallmentLoanContracts.RemoveRange(borrower1);
                    var borrower2 = DbContext.LoanContracts.Where(t => t.BorrowerID == borrower.BorrowerID);
                    DbContext.LoanContracts.RemoveRange(borrower2);              
                    DbContext.Borrowers.Remove(borrower);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return borrower;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public Borrower Create(Borrower borrower)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.Borrowers.Add(borrower);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return borrower;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public override IEnumerable<Borrower> GetAll()
        {
            return this.DbContext.Borrowers;
        }

        public Borrower GetBorrowerByBorrowerID(int ID)
        {
            var borrower = DbContext.Borrowers.Where(u => u.BorrowerID == ID).FirstOrDefault();
            return borrower;
        }

        public Borrower Edit(Borrower record)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                DbContext.Entry(record).State = EntityState.Modified;
                DbContext.SaveChanges();
                transaction.Commit();
                return record;
            }

        }

        public Borrower GetBorrowerByBorrewID(int ID)
        {
            var borrower = DbContext.Borrowers.Where(u => u.BorrowerID == ID).FirstOrDefault();
            return borrower;
        }
    }
    public interface IUBorrowerRepository : IRepository<Borrower>
    {

        Borrower Create(Borrower borrower);
        Borrower Edit(Borrower record);
        Borrower Delete(Borrower borrower);
        Borrower GetBorrowerByBorrewID(int ID);
        Borrower GetBorrowerByBorrowerID(int iD);
    }
}
