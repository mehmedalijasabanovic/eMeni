using eMeni.Domain.Common;
using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Domain.Entities.Business
{
    public class BusinessProfileEntity:BaseEntity
    {
        public int UserId { get; set; }
        public eMeniUserEntity User { get; set; }
        public int PackageId { get; set; }
        public PackageEntity Package { get; set; }
        public ICollection<BusinessEntity> Businesses { get; set; }=new List<BusinessEntity>();
    }
}
