using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class ContributionTransactionHistoryService : IContributionTransactionHistoryService
    {
        private readonly IContributionTransactionHistoryRepository ContributionTransactionHistoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public ContributionTransactionHistoryService(IContributionTransactionHistoryRepository ContributionTransactionHistoryRepository, IUnitOfWork unitOfWork)
        {
            this.ContributionTransactionHistoryRepository = ContributionTransactionHistoryRepository;
            this.unitOfWork = unitOfWork;

        }

        public void Create(ContributionTransactionHistory contributionTransactionHistory)
        {
            ContributionTransactionHistoryRepository.Create(contributionTransactionHistory);
        }

        public void Edit(ContributionTransactionHistory contributionTransactionHistory)
        {
            ContributionTransactionHistoryRepository.Edit(contributionTransactionHistory);
        }

        public IEnumerable<ContributionTransactionHistory> GetAllContributionTransactionHistories()
        {
            return ContributionTransactionHistoryRepository.GetAll();
        }

        public ContributionTransactionHistory GetContributionTransactionHistoryByContractID(int ID)
        {
            return ContributionTransactionHistoryRepository.GetContributionTransactionHistoryByContractID(ID);
        }
        public void Delete(ContributionTransactionHistory contributionTransactionHistory)
        {
            ContributionTransactionHistoryRepository.Delete(contributionTransactionHistory);
        }

        public IEnumerable<User> GetAllUser()
        {
            return ContributionTransactionHistoryRepository.GetAllUser();
        }

        public IEnumerable<ContributionContract> GetAllContributionContract()
        {
            return ContributionTransactionHistoryRepository.GetAllContributionContract();
        }
    }
    public interface IContributionTransactionHistoryService
    {
        void Delete(ContributionTransactionHistory contributionTransactionHistory);
        void Edit(ContributionTransactionHistory contributionTransactionHistory);
        ContributionTransactionHistory GetContributionTransactionHistoryByContractID(int ID);
        IEnumerable<ContributionTransactionHistory> GetAllContributionTransactionHistories();
        void Create(ContributionTransactionHistory contributionTransactionHistory);
        IEnumerable<User> GetAllUser();
        IEnumerable<ContributionContract> GetAllContributionContract();
    }
}