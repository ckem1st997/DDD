using DDD.Domain.Entity;
using DDD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.EntityConfigurations
{
    class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> productConfiguration)
        {
            productConfiguration.ToTable("Products");

            productConfiguration.HasKey(b => b.Id);

            productConfiguration.Ignore(b => b.DomainEvents);
            // thuật toán tạo khoá
            //productConfiguration.Property(b => b.Id)
            //    .UseHiLo("productseq", ProductsContext.DEFAULT_SCHEMA);
            productConfiguration.Property(b => b.Name).IsRequired().HasMaxLength(50);
            productConfiguration.Property(b => b.Price).HasColumnType("decimal(18, 0)");

        }
    }
}
