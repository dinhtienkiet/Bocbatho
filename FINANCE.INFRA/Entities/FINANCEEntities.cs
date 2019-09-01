using Microsoft.EntityFrameworkCore;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Configuration;
using System.Collections.Generic;

namespace FINANCE.INFRA.Entities
{
    public class FINANCEEntities : DbContext
    {
        internal IEnumerable<User> user;

        public FINANCEEntities(DbContextOptions<FINANCEEntities> options) : base(options) { }

        public DbSet<LoanContract> LoanContracts { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<LoanTransactionHistory> LoanTransactionHistories { get; set; }
        public DbSet<InstallmentLoanTransactionHistory> InstallmentLoanTransactionHistories { get; set; }
        public DbSet<ContributionTransactionHistory> ContributionTransactionHistories { get; set; }
        public DbSet<InstallmentLoanContract> InstallmentLoanContracts { get; set; }
        public DbSet<ContributionContract> ContributionContracts { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<User> Users { get; set; }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        //Configuaration for code first
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Master Table Config
            modelBuilder.ApplyConfiguration(new LoanContractConfiguration());
            modelBuilder.ApplyConfiguration(new BorrowerConfiguration());
            modelBuilder.ApplyConfiguration(new LoanTransactionHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new InstallmentLoanTransactionHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new ContributionTransactionHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new InstallmentLoanContractConfiguration());
            modelBuilder.ApplyConfiguration(new ContributionContractConfiguration());
            modelBuilder.ApplyConfiguration(new ContributorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //Seed
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
