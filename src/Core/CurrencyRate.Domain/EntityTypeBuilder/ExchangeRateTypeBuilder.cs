using CurrencyRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Domain.EntityTypeBuilder
{
    public class ExchangeRateTypeBuilder : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .IsRequired()
                .HasColumnType("int")
                .UseIdentityColumn(1, 1);
            builder.Property(p => p.CurrencyCode)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(4);
            builder.Property(p => p.Rate)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);
        }
    }
}
