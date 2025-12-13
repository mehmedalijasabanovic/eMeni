using eMeni.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Business.Queries.List
{
    public sealed class ListBusinessQuery:BasePagedQuery<ListBusinessQueryDto>
    {
        public string? City { get; set; }
    }
}
