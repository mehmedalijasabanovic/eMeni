
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class MenuCategoryEntity:BaseEntity 
{
    public int? MenuId { get; set; }

    public string CategoryName { get; set; }

    public int OrderIndex { get; set; }

    public string Description { get; set; }

    public MenuEntity Menu { get; set; }

    public ICollection<MenuItemEntity> MenuItems { get; set; } = new List<MenuItemEntity>();
}