
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class StatisticEntityConfiguration : IEntityTypeConfiguration<StatisticEntity>
    {
        public void Configure(EntityTypeBuilder<StatisticEntity> entity)
        {
            entity.ToTable("Statistic");

            entity.Property(e => e.Date).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.ReservationsCount).HasDefaultValue(0);
            entity.Property(e => e.ViewsCount).HasDefaultValue(0);

            entity.HasOne(d => d.Business).WithMany(p => p.Statistics)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Statistic_Businesses");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StatisticEntity> entity);
    }
}
