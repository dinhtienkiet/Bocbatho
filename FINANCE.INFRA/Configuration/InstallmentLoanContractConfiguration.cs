using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FINANCE.CORE.Models;
using System;

namespace FINANCE.INFRA.Configuration
{
    public class InstallmentLoanContractConfiguration : IEntityTypeConfiguration<InstallmentLoanContract>
    {
        public void Configure(EntityTypeBuilder<InstallmentLoanContract> builder)
        {
            builder.ToTable("InstallmentLoanContract");
            builder.HasKey(r => r.ContractID);
            builder.Property(r => r.BorrowerID).IsRequired();
            builder.Property(r => r.BorrowerID).IsRequired();
            builder.Property(r => r.Amount).IsRequired();
            builder.Property(r => r.InterestRate).IsRequired();
            builder.Property(r => r.ContractSignDate).IsRequired(); 
            builder.Property(r => r.Term).HasDefaultValue(DateTime.Now).IsRequired();
            builder.Property(r => r.DailyInterest).IsRequired();
            builder.Property(r => r.InterestCycle).IsRequired();
            builder.Property(r => r.Paid).IsRequired();
            builder.Property(r => r.Unpaid).IsRequired();
            builder.Property(r => r.Status).HasDefaultValue(0).IsRequired();
            builder.HasOne(r => r.Borrower)
                .WithMany(r => r.InstallmentLoanContract)
                .HasForeignKey(r => r.BorrowerID);
        }
    }
}
