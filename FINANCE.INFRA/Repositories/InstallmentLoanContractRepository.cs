using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class InstallmentLoanContractRepository :  RepositoryBase<InstallmentLoanContract>, IInstallmentLoanContractRepository
    {
        public InstallmentLoanContractRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public InstallmentLoanContract Create(InstallmentLoanContract installmentLoanContract)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.InstallmentLoanContracts.Add(installmentLoanContract);
                    foreach (var TransactionHistories in installmentLoanContract.InstallmentLoanTransactionHistories)
                    {
                        DbContext.InstallmentLoanTransactionHistories.Add(TransactionHistories);
                    }
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return installmentLoanContract;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }


        public InstallmentLoanContract Edit(InstallmentLoanContract installmentLoanContract)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                var Contract = DbContext.InstallmentLoanContracts
                    .Where(u => u.ContractID == installmentLoanContract.ContractID).FirstOrDefault();
                Contract.BorrowerID = installmentLoanContract.BorrowerID;
                Contract.Amount = installmentLoanContract.Amount;
                Contract.InterestRate = installmentLoanContract.InterestRate;
                Contract.ContractSignDate = installmentLoanContract.ContractSignDate;
                Contract.Term = installmentLoanContract.Term;
                Contract.DailyInterest = installmentLoanContract.DailyInterest;
                Contract.InterestCycle = installmentLoanContract.InterestCycle;
                Contract.Paid = installmentLoanContract.Paid;
                Contract.Unpaid = installmentLoanContract.Unpaid;
                Contract.Status = installmentLoanContract.Status;
                DbContext.InstallmentLoanContracts.Attach(Contract);
                DbContext.SaveChanges();
                transaction.Commit();
                return installmentLoanContract;
            }
        }

        public InstallmentLoanContract GetByID(int ID)
        {
            var Contract = DbContext.InstallmentLoanContracts.Include(u => u.Borrower).Where(u => u.ContractID == ID).FirstOrDefault();
            return Contract;
        }
        public override IEnumerable<InstallmentLoanContract> GetAll()
        {
            return this.DbContext.InstallmentLoanContracts.Include(u => u.Borrower);
        }

        public IEnumerable<Borrower> GetAllBorrowers()
        {
            return this.DbContext.Borrowers;
        }

        InstallmentLoanContract IInstallmentLoanContractRepository.Delete(InstallmentLoanContract installmentLoanContract)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var TransactionHistory = DbContext.InstallmentLoanTransactionHistories.Where(tc => tc.InstallmentLoanContractID == installmentLoanContract.ContractID);
                    if (TransactionHistory.Count() != 0)
                    {
                        foreach( var x in TransactionHistory)
                        {
                            DbContext.InstallmentLoanTransactionHistories.Remove(x);
                        }
                    }
                    DbContext.InstallmentLoanContracts.Remove(installmentLoanContract);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return installmentLoanContract;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public IEnumerable<InstallmentLoanTransactionHistory> GetTransactionHistories(int ID)
        {
            return this.DbContext.InstallmentLoanTransactionHistories.Include(u =>u.InstallmentLoanContract).Where(u => u.InstallmentLoanContractID == ID && u.Type == 1);
        }
    }
    public interface IInstallmentLoanContractRepository
    {
        InstallmentLoanContract Create(InstallmentLoanContract installmentLoanContract);
        InstallmentLoanContract Edit(InstallmentLoanContract installmentLoanContract);
        InstallmentLoanContract GetByID(int ID);
        IEnumerable<InstallmentLoanContract> GetAll();
        IEnumerable<Borrower> GetAllBorrowers();
        InstallmentLoanContract Delete(InstallmentLoanContract installmentLoanContract);
        IEnumerable<InstallmentLoanTransactionHistory> GetTransactionHistories(int ID);
    }
}
