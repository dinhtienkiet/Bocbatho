using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;

namespace FINANCE.CORE.Models
{
    public class InstallmentLoanTransactionHistory
    {
        public InstallmentLoanTransactionHistory()
        {
            ContractSignDate = DateTime.Now;
            Type = (int)TypeHistory.INPUT;
        }
        public int UserID { get; set; }
        public int InstallmentLoanContractID { get; set; }
        public int ContractID { get; set; }
        public int Type { get; set; } //common
        public decimal Amount { get; set; }
        public DateTime ContractSignDate { get; set; }
        public string Note { get; set; }
        public virtual InstallmentLoanContract InstallmentLoanContract { get; set; }
        public virtual User User { get; set; }

    }
}
