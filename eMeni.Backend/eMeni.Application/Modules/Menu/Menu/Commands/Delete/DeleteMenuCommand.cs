using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Delete
{
    public sealed class DeleteMenuCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
