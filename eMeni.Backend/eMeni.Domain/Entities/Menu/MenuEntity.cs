
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class MenuEntity:BaseEntity
{

    public int? BusinessId { get; set; }

    public string MenuTitle { get; set; }

    public string MenuDescription { get; set; }

    public byte? PromotionRank { get; set; }

    public BusinessEntity Business { get; set; }

    public ICollection<MenuCategoryEntity> MenuCategories { get; set; } = new List<MenuCategoryEntity>();
    public static class MenuConstraints
    {
        public const int MenuTitleMaxLength = 100;
        public const int MenuDescriptionMaxLength = 400;
    }
}