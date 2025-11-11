
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;


namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class UserEntityConfiguration : IEntityTypeConfiguration<eMeniUserEntity>
    {
        public void Configure(EntityTypeBuilder<eMeniUserEntity> entity)
        {
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.IsAdmin).HasDefaultValue(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsOwner).HasDefaultValue(false);
            entity.Property(e => e.IsUser).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_City");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<eMeniUserEntity> entity);
    }
}
