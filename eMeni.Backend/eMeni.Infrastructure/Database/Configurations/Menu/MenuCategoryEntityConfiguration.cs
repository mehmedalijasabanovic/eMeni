
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class MenuCategoryEntityConfiguration : IEntityTypeConfiguration<MenuCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<MenuCategoryEntity> entity)
        {
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuCategories)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_MenuCategories_Menus");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MenuCategoryEntity> entity);
    }
}
