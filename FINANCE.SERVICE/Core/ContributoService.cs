using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class ContributoService : IContributoService
    {
        private readonly IContributorRepository contributorRepository;
        private readonly IUnitOfWork unitOfWork;
        public ContributoService(IContributorRepository contributorRepository, IUnitOfWork unitOfWork)
        {
            this.contributorRepository = contributorRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateContributor(Contributor contributor)
        {
            contributorRepository.Create(contributor);
            SaveChange();
        }

        public Contributor DeleteContributor(int ID)
        {
            var ContributorTest = contributorRepository.Delete(ID);
            SaveChange();
            return ContributorTest;
        }

        public void EditContributor(Contributor contributor)
        {
            contributorRepository.Edit(contributor);
            SaveChange();
        }

        public IEnumerable<ContributionContract> GetContributionContracts(int ID)
        {
            return contributorRepository.GetContributionContractsByID(ID);
        }

        public Contributor GetContributorByID(int ID)
        {
            return contributorRepository.GetContributorByID(ID);
        }

        public IEnumerable<Contributor> GetContributors()
        {
            return contributorRepository.GetAll();
        }
        public void SaveChange()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<Contributor> SearchContributor(string text)
        {
            var contributors = contributorRepository.SearchContributor(text);
            return contributors;
        }
    }
    public interface IContributoService
    {
        IEnumerable<ContributionContract> GetContributionContracts(int ID);
        IEnumerable<Contributor> GetContributors();
        IEnumerable<Contributor> SearchContributor(string text);
        Contributor GetContributorByID(int ID);
        Contributor DeleteContributor(int ID);
        void EditContributor(Contributor contributor);
        void CreateContributor(Contributor contributor);

    }
}
