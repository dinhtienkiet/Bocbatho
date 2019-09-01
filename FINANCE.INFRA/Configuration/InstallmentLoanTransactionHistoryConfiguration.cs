using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FINANCE.CORE.Models;
using System;

namespace FINANCE.INFRA.Configuration
{
    public class InstallmentLoanTransactionHistoryConfiguration : IEntityTypeConfiguration<InstallmentLoanTransactionHistory>
    {
        public void Configure(EntityTypeBuilder<InstallmentLoanTransactionHistory> builder)
        {
            builder.ToTable("InstallmentLoanTransactionHistory");
            builder.HasKey(r => r.ContractID);
            builder.Property(r => r.Amount).IsRequired();
            builder.Property(r => r.ContractSignDate).IsRequired();
            builder.Property(r => r.Note).IsRequired();
            builder.Property(r => r.Type).HasDefaultValue(0).IsRequired();
            builder.Property(r => r.UserID).IsRequired();
            builder.HasOne(r => r.InstallmentLoanContract)
                .WithMany(r => r.InstallmentLoanTransactionHistories)
                .HasForeignKey(r => r.InstallmentLoanContractID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstallmentLoanTransactionHistory_InstallmentLoanContract");
            builder.HasOne(r => r.User)
                .WithMany(r => r.InstallmentLoanTransactionHistories)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstallmentLoanTransactionHistory_User");

        }
    }
}
