using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class ContributionContractService : IContributionContractService
    {
        private readonly IContributionContractRepository IContributionContractRepository;
        private readonly IUnitOfWork unitOfWork;

        public ContributionContractService(IContributionContractRepository IContributionContractRepository, IUnitOfWork unitOfWork)
        {
            this.IContributionContractRepository = IContributionContractRepository;
            this.unitOfWork = unitOfWork;

        }

        public void CreateContributionContract(ContributionContract entity)
        {
            IContributionContractRepository.Create(entity);
            SaveContributionContract();
        }
        public void DeleteContributionContract(ContributionContract entity)
        {
            IContributionContractRepository.Delete(entity);
            SaveContributionContract();
        }
        public void EditContributionContract(ContributionContract entity)
        {
            IContributionContractRepository.Edit(entity);
            SaveContributionContract();
        }
        public IEnumerable<ContributionContract> GetAllContributionContract()
        {
            return IContributionContractRepository.GetAll();
        }
        public void SaveContributionContract()
        {
            unitOfWork.Commit();
        }
        public ContributionContract GetContributionContractByContractID(int ID)
        {
            return IContributionContractRepository.GetContributionContractByContractID(ID);
        }
        public IEnumerable<Contributor> GetAllContributor()
        {
            return IContributionContractRepository.GetAllContributor();
        }
    }
    public interface IContributionContractService
    {
        IEnumerable<ContributionContract> GetAllContributionContract();
        void CreateContributionContract(ContributionContract entity);
        void DeleteContributionContract(ContributionContract entity);
        void EditContributionContract(ContributionContract entity);
        ContributionContract GetContributionContractByContractID(int ID);
        IEnumerable<Contributor> GetAllContributor();
    }
}
