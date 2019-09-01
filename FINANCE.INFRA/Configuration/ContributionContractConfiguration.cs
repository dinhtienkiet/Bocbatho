using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FINANCE.CORE.Models;

namespace FINANCE.INFRA.Configuration
{
    public class ContributionContractConfiguration : IEntityTypeConfiguration<ContributionContract>
    {
        public void Configure(EntityTypeBuilder<ContributionContract> builder)
        {
            builder.ToTable("ContributionContract");
            builder.HasKey(r => r.ContractID);
            builder.Property(r => r.Amount).IsRequired();
            builder.Property(r => r.ContractSignDate).IsRequired();
            builder.Property(r => r.InterestCycle).IsRequired();
            builder.Property(r => r.InterestRate).IsRequired();
            builder.Property(r => r.Note).IsRequired();
            builder.Property(r => r.NotReceivedInterest).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.ThisTermInterest).IsRequired();
            builder.HasOne(r => r.Contributors).WithMany(r => r.ContributionContracts)
                   .HasForeignKey(r => r.ContributorID);
        }
    }
}
