#nullable disable
using eMeni.Domain.Common;
using eMeni.Domain.Entities.Identity;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class eMeniUserEntity : BaseEntity
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsOwner { get; set; }

    public bool? IsUser { get; set; }

    public int TokenVersion { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public int CityId { get; set; }

    public bool? Active { get; set; }


    public ICollection<AiChatLogEntity> AiChatLogs { get; set; } = new List<AiChatLogEntity>();

    public ICollection<BusinessEntity> Businesses { get; set; } = new List<BusinessEntity>();

    public CityEntity City { get; set; }

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

    public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();

    public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();

    public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();

    public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
}