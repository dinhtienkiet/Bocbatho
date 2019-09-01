using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class LoanContractRepository : RepositoryBase<LoanContract>, ILoanContractRepository
    {
        public LoanContractRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public LoanContract Create(LoanContract LoanContract)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    if(LoanContract.LoanPackage == 0)
                    {
                        LoanContract.InterestPayDate = (LoanContract.Amount + LoanContract.InterestInDebt)*LoanContract.InterestRate;
                        DbContext.LoanContracts.Add(LoanContract);
                        DbContext.SaveChanges();
                        transaction.Commit();
                        return LoanContract;
                    }else
                    {
                        DbContext.LoanContracts.Add(LoanContract);
                        DbContext.SaveChanges();
                        transaction.Commit();
                        return LoanContract;
                    }
                    
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public LoanContract Delete(LoanContract LoanContract)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var TransactionHistory = DbContext.LoanTransactionHistories.Where(tc => tc.LoanContractID == LoanContract.ContractID);
                    DbContext.LoanTransactionHistories.RemoveRange(TransactionHistory);
                    DbContext.LoanContracts.Remove(LoanContract);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return LoanContract;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public LoanContract Edit(LoanContract LoanContract)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.Entry(LoanContract).State = EntityState.Modified;
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return LoanContract;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public IEnumerable<LoanContract> GetAll()
        {
            var list = UpdateMonth();
            return list;
            //return this.DbContext.LoanContracts;
        }

        public IEnumerable<Borrower> GetAllBorrowers()
        {
            return this.DbContext.Borrowers;
        }

        public LoanContract GetByID(int ID)
        {
            var Contract = DbContext.LoanContracts.Include(u => u.Borrower).Where(u => u.ContractID == ID).FirstOrDefault();
            return Contract;
        }

        public IEnumerable<LoanContract> UpdateMonth()
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                var ListLoanContract = DbContext.LoanContracts;

                foreach (LoanContract LoanContract in ListLoanContract)
                {
                    if (DateTime.Now.Day == 28 && LoanContract.Status !=3)
                    {
                        LoanContract.InterestPayDate = (LoanContract.Amount + LoanContract.InterestInDebt)
                            * LoanContract.InterestRate;
                        DbContext.Entry(LoanContract).State = EntityState.Modified;
                        DbContext.SaveChanges();
                        transaction.Commit();
                    }
                }
                return ListLoanContract;
            }
        }
    }
    public interface ILoanContractRepository
    {
        LoanContract Create(LoanContract LoanContract);
        LoanContract Edit(LoanContract LoanContract);
        LoanContract GetByID(int ID);
        IEnumerable<LoanContract> GetAll();
        IEnumerable<Borrower> GetAllBorrowers();
        LoanContract Delete(LoanContract LoanContract);
        /*IEnumerable<LoanContract> UpdateMonth();*/
    }
}
