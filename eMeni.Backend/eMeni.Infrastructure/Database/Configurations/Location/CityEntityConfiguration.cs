
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class CityEntityConfiguration : IEntityTypeConfiguration<CityEntity>
    {
        public void Configure(EntityTypeBuilder<CityEntity> entity)
        {
            entity.ToTable("City");

            entity.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CityEntity> entity);
    }
}
