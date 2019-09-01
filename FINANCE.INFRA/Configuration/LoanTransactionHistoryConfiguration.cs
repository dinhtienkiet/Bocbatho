using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FINANCE.CORE.Models;
using System;

namespace FINANCE.INFRA.Configuration
{
    public class LoanTransactionHistoryConfiguration : IEntityTypeConfiguration<LoanTransactionHistory>
    {
        public void Configure(EntityTypeBuilder<LoanTransactionHistory> builder)
        {
            builder.ToTable("LoanTransactionHistory");
            builder.HasKey(r => r.ContractID);
            builder.Property(r => r.Amount).IsRequired();
            builder.Property(r => r.ContractSignDate).IsRequired();
            builder.Property(r => r.Type).HasDefaultValue(0).IsRequired();
            builder.Property(r => r.UserID).IsRequired();
            builder.HasOne(r => r.LoanContract)
                .WithMany(r => r.LoanTransactionHistories)
                .HasForeignKey(r => r.LoanContractID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanTransactionHistory_LoanContract");
            builder.HasOne(r => r.User)
                .WithMany(r => r.LoanTransactionHistories)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanTransactionHistory_User");

        }
    }
}
