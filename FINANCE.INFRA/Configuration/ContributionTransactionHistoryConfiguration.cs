using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FINANCE.CORE.Models;
using System;

namespace FINANCE.INFRA.Configuration
{
    public class ContributionTransactionHistoryConfiguration : IEntityTypeConfiguration<ContributionTransactionHistory>
    {
        public void Configure(EntityTypeBuilder<ContributionTransactionHistory> builder)
        {
            builder.ToTable("ContributionTransactionHistory");
            builder.HasKey(r => r.ContractID);
            builder.Property(r => r.Amount).IsRequired();
            builder.Property(r => r.ContractSignDate).IsRequired();
            builder.Property(r => r.Note).IsRequired();
            builder.Property(r => r.Type).HasDefaultValue(0).IsRequired();
            builder.Property(r => r.UserID).IsRequired();
            builder.HasOne(r => r.ContributionContract)
                .WithMany(r => r.ContributionTransactionHistories)
                .HasForeignKey(r => r.ContributionContractID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContributionTransactionHistory_ContributionContract");
            builder.HasOne(r => r.User)
                .WithMany(r => r.ContributionTransactionHistories)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContributionTransactionHistory_User");

        }
    }
}
