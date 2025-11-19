using eMeni.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Location.Queries.List
{
    public sealed class ListCityQuery :BasePagedQuery<ListCityQueryDto>
    {
        public string? Search { get; init; }
    }
}
