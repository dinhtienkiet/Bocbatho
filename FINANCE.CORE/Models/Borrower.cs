using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.CORE.Models
{
    public class Borrower
    {
        public Borrower()
        {
            InstallmentLoanContract = new HashSet<InstallmentLoanContract>();
            LoanContract = new HashSet<LoanContract>();
            Status = (int)StatusBorrower.ON_TIME;
        }
        public int BorrowerID { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public string PhoneNumber { get; set; }
        public string IdCardNumber { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public virtual ICollection<InstallmentLoanContract> InstallmentLoanContract { get; set; }
        public virtual ICollection<LoanContract> LoanContract { get; set; }
    }
}
