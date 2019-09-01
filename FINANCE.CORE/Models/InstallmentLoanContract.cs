using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;

namespace FINANCE.CORE.Models
{
   public class InstallmentLoanContract
    {
        public InstallmentLoanContract()
        {
            InstallmentLoanTransactionHistories = new HashSet<InstallmentLoanTransactionHistory>();
            ContractSignDate = DateTime.Now;
            Status = (int)StatusInstallmentLoan.BORROWING;
        }
        public int BorrowerID { get; set; }
        public int ContractID { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime Term { get; set; }
        public decimal DailyInterest { get; set; }
        public int InterestCycle { get; set; }
        public decimal Paid { get; set; }
        public decimal Unpaid { get; set; }
        public int Status { get; set; } //commom
        public virtual Borrower Borrower { get; set; }
        public virtual ICollection<InstallmentLoanTransactionHistory> InstallmentLoanTransactionHistories { get; set; }
    }
}