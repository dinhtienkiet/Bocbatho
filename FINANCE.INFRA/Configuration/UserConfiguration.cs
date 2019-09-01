  using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FINANCE.CORE.Models;

namespace FINANCE.INFRA.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(r => r.UserID);
            builder.Property(r => r.Username).HasMaxLength(30).IsRequired();
            builder.Property(r => r.Password).HasMaxLength(16).IsRequired();
        }
    }
}
