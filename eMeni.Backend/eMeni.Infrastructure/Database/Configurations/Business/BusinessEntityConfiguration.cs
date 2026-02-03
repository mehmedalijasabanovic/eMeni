
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> entity)
        {
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.BusinessName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PromotionRank).HasDefaultValue((byte)0);

            entity.HasOne(d => d.BusinessCategory).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.BusinessCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_BusinessesCategory");

            entity.HasOne(d => d.City).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_City");

            entity.HasOne(d => d.BusinessProfile).
                WithMany(p => p.Businesses).
                HasForeignKey(d => d.BusinessProfileId).
                OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Business_BusinessProfile");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<BusinessEntity> entity);
    }
}
