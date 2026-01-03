
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class BusinessesCategoryEntityConfiguration : IEntityTypeConfiguration<BusinessesCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessesCategoryEntity> entity)
        {
            entity.ToTable("BusinessesCategory");

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.CategoryDescription)
                .HasMaxLength(400);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<BusinessesCategoryEntity> entity);
    }
}
