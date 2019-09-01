using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;

namespace FINANCE.SERVICE.Core
{
    public class LoanTransactionHistoryService : ILoanTransactionHistoryService
    {
        private readonly ILoanTransactionHistoryRepository LoanTransactionHistoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public LoanTransactionHistoryService(ILoanTransactionHistoryRepository LoanTransactionHistoryRepository, IUnitOfWork unitOfWork)
        {
            this.LoanTransactionHistoryRepository = LoanTransactionHistoryRepository;
            this.unitOfWork = unitOfWork;

        }

        public void Create(LoanTransactionHistory loanTransactionHistory)
        {
            LoanTransactionHistoryRepository.Create(loanTransactionHistory);
        }

        public void Edit(LoanTransactionHistory loanTransactionHistory)
        {
            LoanTransactionHistoryRepository.Edit(loanTransactionHistory);
        }

        public IEnumerable<LoanTransactionHistory> GetAllLoanTransactionHistories()
        {
            return LoanTransactionHistoryRepository.GetAll();
        }

        public LoanTransactionHistory GetLoanTransactionHistoryByContractID(int ID)
        {
            return LoanTransactionHistoryRepository.GetLoanTransactionHistoryByContractID(ID);
        }
        public void Delete(LoanTransactionHistory loanTransactionHistory)
        {
            LoanTransactionHistoryRepository.Delete(loanTransactionHistory);
        }

        public IEnumerable<User> GetAllUser()
        {
            return LoanTransactionHistoryRepository.GetAllUser();
        }

        public IEnumerable<LoanContract> GetAllLoanContract()
        {
            return LoanTransactionHistoryRepository.GetAllLoanContract();
        }
    }
    public interface ILoanTransactionHistoryService
    {
        void Delete(LoanTransactionHistory loanTransactionHistory);
        void Edit(LoanTransactionHistory loanTransactionHistory);
        LoanTransactionHistory GetLoanTransactionHistoryByContractID(int ID);
        IEnumerable<LoanTransactionHistory> GetAllLoanTransactionHistories();
        void Create(LoanTransactionHistory loanTransactionHistory);
        IEnumerable<User> GetAllUser();
        IEnumerable<LoanContract> GetAllLoanContract();
    }
}