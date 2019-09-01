using System.Collections.Generic;

namespace FINANCE.CORE.Models
{
    public class User
    {
        public User()
        {
            LoanTransactionHistories = new HashSet<LoanTransactionHistory>();
            InstallmentLoanTransactionHistories = new HashSet<InstallmentLoanTransactionHistory>();
            ContributionTransactionHistories = new HashSet<ContributionTransactionHistory>();
        }
        public int  UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public virtual ICollection<LoanTransactionHistory> LoanTransactionHistories { get; set; }
        public virtual ICollection<InstallmentLoanTransactionHistory> InstallmentLoanTransactionHistories { get; set; }
        public virtual ICollection<ContributionTransactionHistory> ContributionTransactionHistories { get; set; }

    }
    
}
