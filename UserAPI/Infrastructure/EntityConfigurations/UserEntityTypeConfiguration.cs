using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;

namespace UserAPI.Infrastructure.EntityConfigurations
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> productConfiguration)
        {
            productConfiguration.ToTable("Users");
            productConfiguration.HasKey(b => b.Id);
            productConfiguration.Property(b => b.Username).IsRequired().HasMaxLength(20);
            productConfiguration.Property(b => b.Pass).IsRequired().HasMaxLength(20);
            productConfiguration.Property(b => b.Roleu).IsRequired().HasDefaultValueSql("User");
            productConfiguration.Property(b => b.Active).IsRequired().HasDefaultValue(false);

        }
    }
}
