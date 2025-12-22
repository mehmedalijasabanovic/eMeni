using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Commands.Delete
{
    public sealed class DeleteUserCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
