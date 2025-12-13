
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class MenuEntityConfiguration : IEntityTypeConfiguration<MenuEntity>
    {
        public void Configure(EntityTypeBuilder<MenuEntity> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MenuDescription).HasMaxLength(400);
            entity.Property(e => e.MenuTitle).HasMaxLength(100);
            entity.Property(e => e.PromotionRank).HasDefaultValue((byte)0);

            entity.HasOne(d => d.Business).WithMany(p => p.Menus)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_Menus_Business");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MenuEntity> entity);
    }
}
