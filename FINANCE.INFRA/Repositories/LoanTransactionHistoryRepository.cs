using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class LoanTransactionHistoryRepository : RepositoryBase<LoanTransactionHistory>, ILoanTransactionHistoryRepository
    {
        public LoanTransactionHistoryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public LoanTransactionHistory Delete(LoanTransactionHistory loanTransactionHistory)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.LoanTransactionHistories.Remove(loanTransactionHistory);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return loanTransactionHistory;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public LoanTransactionHistory Create(LoanTransactionHistory loanTransactionHistory)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.LoanTransactionHistories.Add(loanTransactionHistory);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return loanTransactionHistory;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public override IEnumerable<LoanTransactionHistory> GetAll()
        {
            return this.DbContext.LoanTransactionHistories.Include(u => u.User);
        }

        public LoanTransactionHistory GetLoanTransactionHistoryByContractID(int ID)
        {
            var transactionHistory = DbContext.LoanTransactionHistories.Include(u =>u.User).Where(u => u.ContractID == ID).FirstOrDefault();
            return transactionHistory;
        }

        public LoanTransactionHistory Edit(LoanTransactionHistory record)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                var transact_1 = DbContext.LoanTransactionHistories.Where(u => u.ContractID == record.ContractID).FirstOrDefault();
                transact_1.UserID = record.UserID;
                transact_1.LoanContractID = record.LoanContractID;
                transact_1.Type = record.Type;
                transact_1.Amount = record.Amount;
                transact_1.ContractSignDate = record.ContractSignDate;
                transact_1.Note = record.Note;
                DbContext.LoanTransactionHistories.Attach(transact_1);
                DbContext.SaveChanges();
                transaction.Commit();
                return record;
            }

        }

        public IEnumerable<User> GetAllUser()
        {
            return DbContext.Users;
        }

        public IEnumerable<LoanContract> GetAllLoanContract()
        {
            return DbContext.LoanContracts;
        }
    }
    public interface ILoanTransactionHistoryRepository : IRepository<LoanTransactionHistory>
    {
        //IEnumerable<User> GetAll();
        LoanTransactionHistory Create(LoanTransactionHistory loanTransactionHistory);
        LoanTransactionHistory Edit(LoanTransactionHistory record);
        LoanTransactionHistory Delete(LoanTransactionHistory loanTransactionHistory);
        LoanTransactionHistory GetLoanTransactionHistoryByContractID(int ID);
        IEnumerable<User> GetAllUser();
        IEnumerable<LoanContract> GetAllLoanContract();
    }
}