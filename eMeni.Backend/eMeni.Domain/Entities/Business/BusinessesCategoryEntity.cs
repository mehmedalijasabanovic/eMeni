
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class BusinessesCategoryEntity : BaseEntity
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }

    public ICollection<BusinessEntity> Businesses { get; set; } = new List<BusinessEntity>();
    public static class Constraint
    {
        public const int NameMaxLenght  = 40;
    }
}