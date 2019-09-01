using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.CORE.Models
{
    public class LoanContract
    {
        public LoanContract()
        {
            LoanTransactionHistories = new HashSet<LoanTransactionHistory>();
            ContractSignDate = DateTime.Now;
            LoanPackage = (int)Loan_Package.LENGHT;
            Status = (int)StatusLoan.ENOUGH;
        }
        public int BorrowerID { get; set; }
        public int ContractID { get; set; }
        public int LoanPackage { get; set; } 
        public decimal InterestRate { get; set; }
        public decimal Amount { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal InterestPayDate { get; set; }
        public decimal InterestInDebt { get; set; }
        public int Status { get; set; } 
        public string Note { get; set; }
        public virtual Borrower Borrower { get; set; }
        public virtual ICollection<LoanTransactionHistory> LoanTransactionHistories { get; set; }
    }
}
