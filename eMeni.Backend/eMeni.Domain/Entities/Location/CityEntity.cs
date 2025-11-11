
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class CityEntity : BaseEntity
{
    public string CityName { get; set; }

    public ICollection<BusinessEntity> Businesses { get; set; } = new List<BusinessEntity>();

    public ICollection<eMeniUserEntity> Users { get; set; } = new List<eMeniUserEntity>();
}