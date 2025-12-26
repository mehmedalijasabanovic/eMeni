
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class MenuItemEntity:BaseEntity
{

    public int? CategoryId { get; set; }

    public string ItemName { get; set; }

    public string ItemDescription { get; set; }

    public string Price { get; set; }

    public string ImageUrl { get; set; }

  
    public MenuCategoryEntity Category { get; set; }
    public static class MenuItemConstraints
    {
        public const int ItemNameMaxLength = 50;
        public const int ItemDescriptionMaxLength = 200;
        public const int PriceMaxLength = 20;
        public const int ImageUrlMaxLength = 200;
    }
}