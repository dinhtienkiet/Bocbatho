using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.INFRA.Repositories
{
    public class ContributorRepository : RepositoryBase<Contributor>, IContributorRepository
    {
        public ContributorRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Contributor Create(Contributor contributor)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.Add(contributor);
                    DbContext.SaveChanges();
                    transaction.Commit();
                    return contributor;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }

        }
        public IEnumerable<ContributionContract> GetContributionContractsByID(int ID)
        {
            var ContributionContracts = DbContext.ContributionContracts.Include(c => c.Contributors).Where(c => c.ContributorID == ID).ToList();
            return ContributionContracts;
        }

        public Contributor Edit(Contributor contributor)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var contributorTest = DbContext.Contributors.
                        Where(c => c.ContributorID == contributor.ContributorID)
                        .FirstOrDefault();
                    contributorTest.DoB = contributor.DoB;
                    contributorTest.BankNumber = contributor.BankNumber;
                    contributorTest.Name = contributor.Name;
                    contributorTest.PhoneNumber = contributor.PhoneNumber;
                    contributorTest.IdCardNumber = contributor.IdCardNumber;
                    DbContext.Attach(contributorTest);
                    DbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }
            return contributor;
        }

        public override IEnumerable<Contributor> GetAll()
        {
            return this.DbContext.Contributors;
        }

        public Contributor GetContributorByID(int ID)
        {
            return this.DbContext.Contributors
                .Where(c => c.ContributorID == ID)
                .FirstOrDefault();
        }
        public IEnumerable<Contributor> SearchContributor(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var contributorTest = DbContext.Contributors
               .Where(c => c.ContributorID.ToString().Contains(text) ||
               c.Name.Contains(text) || c.IdCardNumber.ToString().Contains(text) || c.PhoneNumber.ToString().Contains(text)).ToList();
                return contributorTest;
            }
            return this.DbContext.Contributors;
        }

        public Contributor Delete(int ID)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var contributor = DbContext.Contributors.Where(u => u.ContributorID == ID).FirstOrDefault();
                    var contributorContract = DbContext.ContributionContracts.Where(u => u.ContributorID == ID).FirstOrDefault();
                    if (contributorContract == null)
                    {
                        DbContext.Remove(contributor);
                        DbContext.SaveChanges();
                        transaction.Commit();
                        return contributor;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }

        }
    }
    public interface IContributorRepository : IRepository<Contributor>
    {
        Contributor Create(Contributor contributor);
        Contributor Edit(Contributor contributor);
        Contributor Delete(int ID);
        Contributor GetContributorByID(int ID);
        IEnumerable<ContributionContract> GetContributionContractsByID(int ID);
        IEnumerable<Contributor> SearchContributor(String text);
    }

}
