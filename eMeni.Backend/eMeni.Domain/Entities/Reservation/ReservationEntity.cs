
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class ReservationEntity:BaseEntity
{
 

    public int? UserId { get; set; }

    public int BusinessId { get; set; }

    public string UserEmail { get; set; }

    public DateTime ReservationDate { get; set; }

    public int? NumberOfPeople { get; set; }

    public string Notes { get; set; }

    public BusinessEntity Business { get; set; }

    public eMeniUserEntity User { get; set; }
}