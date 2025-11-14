
using eMeni.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models.Configurations
{
    public partial class AiChatLogEntityConfiguration : IEntityTypeConfiguration<AiChatLogEntity>
    {
        public void Configure(EntityTypeBuilder<AiChatLogEntity> entity)
        {
            entity.Property(e => e.Message).IsRequired();
            entity.Property(e => e.MessageType)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.SessionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.AiChatLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AiChatLog__UserI__6B24EA82");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AiChatLogEntity> entity);
    }
}
