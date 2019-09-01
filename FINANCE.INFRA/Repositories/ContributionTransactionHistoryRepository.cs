using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class ContributionTransactionHistoryRepository : RepositoryBase<ContributionTransactionHistory>, IContributionTransactionHistoryRepository
    {
        public ContributionTransactionHistoryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public ContributionTransactionHistory Delete(ContributionTransactionHistory contributionTransactionHistory)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.ContributionTransactionHistories.Remove(contributionTransactionHistory);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return contributionTransactionHistory;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public ContributionTransactionHistory Create(ContributionTransactionHistory contributionTransactionHistory)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.ContributionTransactionHistories.Add(contributionTransactionHistory);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return contributionTransactionHistory;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public override IEnumerable<ContributionTransactionHistory> GetAll()
        {
            return this.DbContext.ContributionTransactionHistories.Include(u => u.User);
        }

        public ContributionTransactionHistory GetContributionTransactionHistoryByContractID(int ID)
        {
            var transactionHistory = DbContext.ContributionTransactionHistories.Include(u => u.User).Where(u => u.ContractID == ID).FirstOrDefault();
            return transactionHistory;
        }

        public ContributionTransactionHistory Edit(ContributionTransactionHistory record)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                var transact_1 = DbContext.ContributionTransactionHistories.Where(u => u.ContractID == record.ContractID).FirstOrDefault();
                transact_1.UserID = record.UserID;
                transact_1.ContributionContractID = record.ContributionContractID;
                transact_1.Type = record.Type;
                transact_1.Amount = record.Amount;
                transact_1.ContractSignDate = record.ContractSignDate;
                transact_1.Note = record.Note;
                DbContext.ContributionTransactionHistories.Attach(transact_1);
                DbContext.SaveChanges();
                transaction.Commit();
                return record;
            }

        }

        public IEnumerable<User> GetAllUser()
        {
            return DbContext.Users;
        }

        public IEnumerable<ContributionContract> GetAllContributionContract()
        {
            return DbContext.ContributionContracts;
        }
    }
    public interface IContributionTransactionHistoryRepository : IRepository<ContributionTransactionHistory>
    {
        //IEnumerable<User> GetAll();
        ContributionTransactionHistory Create(ContributionTransactionHistory contributionTransactionHistory);
        ContributionTransactionHistory Edit(ContributionTransactionHistory record);
        ContributionTransactionHistory Delete(ContributionTransactionHistory contributionTransactionHistory);
        ContributionTransactionHistory GetContributionTransactionHistoryByContractID(int ID);
        IEnumerable<User> GetAllUser();
        IEnumerable<ContributionContract> GetAllContributionContract();
    }
}