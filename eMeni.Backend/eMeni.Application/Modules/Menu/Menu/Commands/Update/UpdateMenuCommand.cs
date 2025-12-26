using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Update
{
    public sealed class UpdateMenuCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public string MenuTitle { get; set; }
        public string MenuDescription { get; set; }
    }
}
