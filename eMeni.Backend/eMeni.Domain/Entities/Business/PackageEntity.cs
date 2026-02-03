
#nullable disable
using eMeni.Domain.Common;
using System;
using System.Collections.Generic;

namespace eMeni.Infrastructure.Models;

public sealed class PackageEntity:BaseEntity
{

    public string PackageName { get; set; }

    public byte MaxImages { get; set; }

    public byte MaxMenus { get; set; }

    public int Price { get; set; }

    public string Description { get; set; }

}