using eMeni.Domain.Entities.Business;
using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Infrastructure.Database.Configurations.Business
{
     public partial class BusinessProfileEntityConfiguration : IEntityTypeConfiguration<BusinessProfileEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessProfileEntity> entity)
        {
            entity.HasOne(x => x.User).
            WithMany().
            HasForeignKey(x => x.UserId).
            OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BusinessProfile_User");

            entity.HasOne(x => x.Package).
             WithMany().
             HasForeignKey(x => x.PackageId).
            OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BusinessProfile_Package");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<BusinessProfileEntity> entity);
    }
}
