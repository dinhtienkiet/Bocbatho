using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FINANCE.INFRA.Repositories
{
    public class ContributionContractRepository : RepositoryBase<ContributionContract>, IContributionContractRepository
    {
        public ContributionContractRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public ContributionContract Create(ContributionContract entity)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.ContributionContracts.Add(entity);
                    foreach (var TransactionHistories in entity.ContributionTransactionHistories)
                    {
                        DbContext.ContributionTransactionHistories.Add(TransactionHistories);
                    }
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return entity;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public ContributionContract DeleteContributionContract(ContributionContract entity)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var Contribute = DbContext.ContributionTransactionHistories.Where(r => r.ContributionContractID == entity.ContractID).FirstOrDefault();
                    if (Contribute != null)
                    {
                        DbContext.ContributionTransactionHistories.Remove(Contribute);
                    }
                    DbContext.ContributionContracts.Remove(entity);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return entity;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }

        public ContributionContract Edit(ContributionContract entity)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var contract = DbContext.ContributionContracts.Where(r => r.ContributorID == entity.ContributorID).FirstOrDefault();
                    contract.ContractID = entity.ContractID;
                    contract.Amount = entity.Amount;
                    contract.ContractSignDate = entity.ContractSignDate;
                    contract.ExpireDate = entity.ExpireDate;
                    contract.InterestCycle = entity.InterestCycle;
                    contract.InterestRate = entity.InterestRate;
                    contract.Note = entity.Note;
                    contract.NotReceivedInterest = entity.NotReceivedInterest;
                    contract.Status = entity.Status;
                    contract.ThisTermInterest = entity.ThisTermInterest;
                    DbContext.ContributionContracts.Attach(contract);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return contract;
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public IEnumerable<Contributor> GetAllContributor()
        {
            return this.DbContext.Contributors;
        }
        public ContributionContract GetContributionContractByContractID(int ID)
        {
            var contributor = DbContext.ContributionContracts.Include(r => r.Contributors).Where(c => c.ContractID == ID).FirstOrDefault();
            return contributor;
        }
        public override IEnumerable<ContributionContract> GetAll()
        {
            return this.DbContext.ContributionContracts.Include(r => r.Contributors);
        }
    }

    public interface IContributionContractRepository : IRepository<ContributionContract>
    {
        ContributionContract Create(ContributionContract entity);
        ContributionContract DeleteContributionContract(ContributionContract entity);
        ContributionContract Edit(ContributionContract entity);
        ContributionContract GetContributionContractByContractID(int ID);
        IEnumerable<Contributor> GetAllContributor();
    }
}
