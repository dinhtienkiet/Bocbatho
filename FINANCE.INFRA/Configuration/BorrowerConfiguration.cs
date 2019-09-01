using FINANCE.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINANCE.INFRA.Configuration
{
    public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            builder.ToTable("Borrower");
            builder.HasKey(r => r.BorrowerID);
            builder.Property(r => r.Name).HasMaxLength(250).IsRequired();
            builder.Property(r => r.DoB).IsRequired();
            builder.Property(r => r.PhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(r => r.IdCardNumber).HasMaxLength(20).IsRequired();
            builder.Property(r => r.Address).HasMaxLength(250).IsRequired();
            builder.Property(r => r.Status).HasDefaultValue(1).IsRequired();

        }
    }
}
