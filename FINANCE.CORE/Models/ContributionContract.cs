using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.CORE.Models
{
    public class ContributionContract
    {
        public ContributionContract()
        {
            ContractSignDate = DateTime.Now;
            ContributionTransactionHistories = new HashSet<ContributionTransactionHistory>();
        }
        public int ContributorID { get; set; }
        public int ContractID { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int InterestCycle { get; set; }
        public decimal ThisTermInterest { get; set; }
        public decimal NotReceivedInterest { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public virtual Contributor Contributors { get; set; }
        public virtual ICollection<ContributionTransactionHistory> ContributionTransactionHistories { get; set; }
    }
}
