using eMeni.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Queries.List
{
    public sealed class ListBusinessesCategoryQuery:BasePagedQuery<ListBusinessesCategoryQueryDto>
    {
        public string? Search { get; init; }
    }
}
