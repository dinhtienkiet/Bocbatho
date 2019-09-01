using System;
using FINANCE.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINANCE.INFRA.Configuration
{
    public class LoanContractConfiguration : IEntityTypeConfiguration<LoanContract>
    {
        public void Configure(EntityTypeBuilder<LoanContract> builder)
        {
            builder.ToTable("LoanContract");
            builder.HasKey(r => r.ContractID);
            builder.Property(r => r.BorrowerID).IsRequired();
            builder.Property(r => r.LoanPackage).HasDefaultValue(0).IsRequired();
            builder.Property(r => r.InterestRate).IsRequired();
            builder.Property(r => r.Amount).IsRequired();
            builder.Property(r => r.ContractSignDate).IsRequired();
            builder.Property(r => r.ExpireDate).IsRequired();
            builder.Property(r => r.InterestPayDate).IsRequired();
            builder.Property(r => r.Status).HasDefaultValue(0).IsRequired();
            builder.Property(r => r.Note).HasMaxLength(500).IsRequired();
            builder.HasOne(r => r.Borrower)
                .WithMany(r => r.LoanContract)
                .HasForeignKey(r => r.BorrowerID);

        }
    }
}
