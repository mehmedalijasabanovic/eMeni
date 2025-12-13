
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class BusinessEntity : BaseEntity
{
    public string BusinessName { get; set; }

    public int BusinessCategoryId { get; set; }

    public int? PackageId { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public int CityId { get; set; }

    public BusinessesCategoryEntity BusinessCategory { get; set; }

    public CityEntity City { get; set; }

    public ICollection<MenuEntity> Menus { get; set; } = new List<MenuEntity>();

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

    public PackageEntity Package { get; set; }

    public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();

    public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();

    public ICollection<StatisticEntity> Statistics { get; set; } = new List<StatisticEntity>();

    public eMeniUserEntity User { get; set; }
    public static class Constraint
    {
        public const int AddressMaxLength = 100;
        public const int BusinessNameMaxLength = 100;
        public const int DescriptionMaxLength = 400;
    }

}