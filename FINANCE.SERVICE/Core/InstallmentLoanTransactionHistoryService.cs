using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class InstallmentLoanTransactionHistoryService : IInstallmentLoanTransactionHistoryService
    {
        private readonly IInstallmentLoanTransactionHistoryRepository InstallmentLoanTransactionHistoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public InstallmentLoanTransactionHistoryService(IInstallmentLoanTransactionHistoryRepository InstallmentLoanTransactionHistoryRepository, IUnitOfWork unitOfWork)
        {
            this.InstallmentLoanTransactionHistoryRepository = InstallmentLoanTransactionHistoryRepository;
            this.unitOfWork = unitOfWork;

        }

        public void Create(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            InstallmentLoanTransactionHistoryRepository.Create(installmentLoanTransactionHistory);
        }

        public void Edit(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            InstallmentLoanTransactionHistoryRepository.Edit(installmentLoanTransactionHistory);
        }

        public IEnumerable<InstallmentLoanTransactionHistory> GetAllInstallmentLoanTransactionHistories()
        {
            return InstallmentLoanTransactionHistoryRepository.GetAll();
        }

        public InstallmentLoanTransactionHistory GetInstallmentLoanTransactionHistoryByContractID(int ID)
        {
            return InstallmentLoanTransactionHistoryRepository.GetInstallmentLoanTransactionHistoryByContractID(ID);
        }
        public void Delete(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            InstallmentLoanTransactionHistoryRepository.Delete(installmentLoanTransactionHistory);
        }
        public IEnumerable<User> GetAllUser()
        {
            return InstallmentLoanTransactionHistoryRepository.GetAllUser();
        }

        public IEnumerable<InstallmentLoanContract> GetAllInstallmentLoanContract()
        {
            return InstallmentLoanTransactionHistoryRepository.GetAllInstallmentLoanContract();
        }

    }
    public interface IInstallmentLoanTransactionHistoryService
    {
        void Delete(InstallmentLoanTransactionHistory installmentLoanTransactionHistory);
        void Edit(InstallmentLoanTransactionHistory installmentLoanTransactionHistory);
        InstallmentLoanTransactionHistory GetInstallmentLoanTransactionHistoryByContractID(int ID);
        IEnumerable<InstallmentLoanTransactionHistory> GetAllInstallmentLoanTransactionHistories();
        void Create(InstallmentLoanTransactionHistory installmentLoanTransactionHistory);
        IEnumerable<User> GetAllUser();
        IEnumerable<InstallmentLoanContract> GetAllInstallmentLoanContract();
    }
}