using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.CORE.Models
{
    public class Contributor
    {
        public Contributor()
        {
            ContributionContracts = new HashSet<ContributionContract>();
        }
        public int ContributorID { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public int PhoneNumber { get; set; }
        public int IdCardNumber { get; set; }
        public int BankNumber { get; set; }
        public virtual ICollection<ContributionContract> ContributionContracts { get; set; }
    }
}
