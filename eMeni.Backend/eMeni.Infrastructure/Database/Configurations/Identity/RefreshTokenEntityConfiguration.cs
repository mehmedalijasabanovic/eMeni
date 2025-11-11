
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> entity)
        {
            entity.Property(e => e.Fingerprint).HasMaxLength(200);
            entity.Property(e => e.TokenHash)
                .IsRequired()
                .HasMaxLength(450);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<RefreshTokenEntity> entity);
    }
}
