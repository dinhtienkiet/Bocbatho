using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;
namespace FINANCE.SERVICE.Core
{
    public class InstallmentLoanContractService : IInstallmentLoanContractService
    {
        public readonly IInstallmentLoanContractRepository installmentloanRepository;
        private readonly IUnitOfWork unitOfWork;
        public InstallmentLoanContractService(IInstallmentLoanContractRepository installmentloanRepository, IUnitOfWork unitOfWork)
        {
            this.installmentloanRepository = installmentloanRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(InstallmentLoanContract InstallmentLoanContract)
        {
            installmentloanRepository.Create(InstallmentLoanContract);
            SaveInstallmentLoanContract();
        }

        public IEnumerable<InstallmentLoanContract> GetAll()
        {
            return installmentloanRepository.GetAll();
        }

        public InstallmentLoanContract GetByID(int ID)
        {
            var InstallmentLoanContract = installmentloanRepository.GetByID(ID);
            return InstallmentLoanContract;
        }

        void IInstallmentLoanContractService.Create(InstallmentLoanContract InstallmentLoanContract)
        {
            installmentloanRepository.Create(InstallmentLoanContract);
            SaveInstallmentLoanContract();
        }
        public void SaveInstallmentLoanContract()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<Borrower> GetAllBorrowers()
        {
            return installmentloanRepository.GetAllBorrowers();
        }

        public void Delete(InstallmentLoanContract InstallmentLoanContract)
        {
            installmentloanRepository.Delete(InstallmentLoanContract);
            SaveInstallmentLoanContract();
        }

        public void Edit(InstallmentLoanContract InstallmentLoanContract)
        {
            installmentloanRepository.Edit(InstallmentLoanContract);
            SaveInstallmentLoanContract();
        }

        public IEnumerable<InstallmentLoanTransactionHistory> GetTransactionHistories(int ID)
        {
            return installmentloanRepository.GetTransactionHistories(ID);
        }
    }
    public interface IInstallmentLoanContractService
    {
        void Create(InstallmentLoanContract InstallmentLoanContract);
        void Delete(InstallmentLoanContract InstallmentLoanContract);
        void Edit(InstallmentLoanContract InstallmentLoanContract);
        InstallmentLoanContract GetByID(int ID);
        IEnumerable<InstallmentLoanContract> GetAll();
        IEnumerable<Borrower> GetAllBorrowers();
        IEnumerable<InstallmentLoanTransactionHistory> GetTransactionHistories(int ID);
    }
}
