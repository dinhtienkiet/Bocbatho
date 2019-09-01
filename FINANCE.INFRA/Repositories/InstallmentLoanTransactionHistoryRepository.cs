using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class InstallmentLoanTransactionHistoryRepository : RepositoryBase<InstallmentLoanTransactionHistory>, IInstallmentLoanTransactionHistoryRepository
    {
        public InstallmentLoanTransactionHistoryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public InstallmentLoanTransactionHistory Delete(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.InstallmentLoanTransactionHistories.Remove(installmentLoanTransactionHistory);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return installmentLoanTransactionHistory;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public InstallmentLoanTransactionHistory Create(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.InstallmentLoanTransactionHistories.Add(installmentLoanTransactionHistory);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return installmentLoanTransactionHistory;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public override IEnumerable<InstallmentLoanTransactionHistory> GetAll()
        {
            return this.DbContext.InstallmentLoanTransactionHistories.Include(u => u.User);
        }

        public InstallmentLoanTransactionHistory GetInstallmentLoanTransactionHistoryByContractID(int ID)
        {
            var transactionHistory = DbContext.InstallmentLoanTransactionHistories.Include(u => u.User).Where(u => u.ContractID == ID).FirstOrDefault();
            return transactionHistory;
        }

        public InstallmentLoanTransactionHistory Edit(InstallmentLoanTransactionHistory record)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                var transact_1 = DbContext.InstallmentLoanTransactionHistories.Where(u => u.ContractID == record.ContractID).FirstOrDefault();
                transact_1.UserID = record.UserID;
                transact_1.InstallmentLoanContractID = record.InstallmentLoanContractID;
                transact_1.Type = record.Type;
                transact_1.Amount = record.Amount;
                transact_1.ContractSignDate = record.ContractSignDate;
                transact_1.Note = record.Note;
                DbContext.InstallmentLoanTransactionHistories.Attach(transact_1);
                DbContext.SaveChanges();
                transaction.Commit();
                return record;
            }

        }

        public IEnumerable<User> GetAllUser()
        {
            return DbContext.Users;
        }

        public IEnumerable<InstallmentLoanContract> GetAllInstallmentLoanContract()
        {
            return DbContext.InstallmentLoanContracts;
        }

    }
    public interface IInstallmentLoanTransactionHistoryRepository : IRepository<InstallmentLoanTransactionHistory>
    {
        //IEnumerable<User> GetAll();
        InstallmentLoanTransactionHistory Create(InstallmentLoanTransactionHistory installmentLoanTransactionHistory);
        InstallmentLoanTransactionHistory Edit(InstallmentLoanTransactionHistory record);
        InstallmentLoanTransactionHistory Delete(InstallmentLoanTransactionHistory installmentLoanTransactionHistory);
        InstallmentLoanTransactionHistory GetInstallmentLoanTransactionHistoryByContractID(int ID);
        IEnumerable<User> GetAllUser();
        IEnumerable<InstallmentLoanContract> GetAllInstallmentLoanContract();
    }
}