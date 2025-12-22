using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Queries.GetById
{
    public sealed class GetUserByIdQuery:IRequest<GetUserByIdQueryDto>
    {
        public int Id { get; set; }
    }
}
