using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class LoanContractService : ILoanContractService
    {
        private readonly ILoanContractRepository loanContractRepository;
        public readonly IUnitOfWork unitOfWork;
        public LoanContractService(ILoanContractRepository loanContractService, IUnitOfWork unitOfWork)
        {
            this.loanContractRepository = loanContractService;
            this.unitOfWork = unitOfWork;
        }
        public void Create(LoanContract LoanContract)
        {
            loanContractRepository.Create(LoanContract);
            SaveLoanContract();
        }

        public void Delete(LoanContract LoanContract)
        {
            loanContractRepository.Delete(LoanContract);
            SaveLoanContract();
        }

        public void Edit(LoanContract LoanContract)
        {
            loanContractRepository.Edit(LoanContract);
            SaveLoanContract();
        }

        public IEnumerable<LoanContract> GetAll()
        {
            return loanContractRepository.GetAll();
        }

        public IEnumerable<Borrower> GetAllBorrowers()
        {
            return loanContractRepository.GetAllBorrowers();
        }

        public LoanContract GetByID(int ID)
        {
            return loanContractRepository.GetByID(ID);
        }
        public void SaveLoanContract()
        {
            unitOfWork.Commit();
        }
    }
    public interface ILoanContractService
    {
        void Create(LoanContract LoanContract);
        void Delete(LoanContract LoanContract);
        void Edit(LoanContract LoanContract);
        LoanContract GetByID(int ID);
        IEnumerable<LoanContract> GetAll();
        IEnumerable<Borrower> GetAllBorrowers();
    }
}
