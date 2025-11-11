
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class QRCodeProductEntity:BaseEntity
{

    public string ProductName { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public string MaterialType { get; set; }

    public string Size { get; set; }

    public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
}