using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;

namespace FINANCE.CORE.Models
{
    public class LoanTransactionHistory
    {
        public LoanTransactionHistory()
        {
            ContractSignDate = DateTime.Now;
            Type = (int)TypeHistory.INPUT;
        }
        public int UserID { get; set; }
        public int LoanContractID { get; set; }
        public int ContractID { get; set; }
        public int Type { get; set; } //common
        public decimal Amount { get; set; }
        public DateTime ContractSignDate { get; set; }
        public string Note { get; set; }
        public virtual LoanContract LoanContract { get; set; }
        public virtual User User { get; set; }

    }
}
