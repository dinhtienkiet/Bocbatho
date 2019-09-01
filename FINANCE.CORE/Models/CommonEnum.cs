using System;
using System.Collections.Generic;
using System.Text;

namespace FINANCE.CORE.Models
{
    public enum Loan_Package
    {
        LENGHT = 0,
        SHORTS = 1
    }
    public enum StatusBorrower
    {
        BAD_DEBT = 0,
        ON_TIME = 1 
    }
    public enum StatusInstallmentLoan
    {
        BORROWING = 0,
        DEBT = 1,
        OVERDUE= 2,
        CLOSED = 3
    }
    public enum StatusLoan
    {
         ENOUGH = 0, 
         OWED= 1, 
         OVERDUE = 2,
         CLOSED= 3
    }
    public enum TypeHistory
    {
        INPUT = 0, 
        OUTPUT = 1 
    }

}
