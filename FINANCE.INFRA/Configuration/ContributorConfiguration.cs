using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINANCE.INFRA.Configuration
{
    public class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
    {
        public void Configure(EntityTypeBuilder<Contributor> builder)
        {
            builder.ToTable("Contributor");
            builder.HasKey(r => r.ContributorID);
            builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
            builder.Property(r => r.DoB).IsRequired();
            builder.Property(r => r.IdCardNumber).IsRequired();
            builder.Property(r => r.PhoneNumber).IsRequired();
        }
    }
}
