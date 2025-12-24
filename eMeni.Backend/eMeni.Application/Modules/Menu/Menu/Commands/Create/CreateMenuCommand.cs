using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Create
{
    public sealed class CreateMenuCommand:IRequest<int>
    {
        public int BusinessId { get; set; }
        public string MenuTitle { get; set; }
        public string MenuDescription { get; set; }
    }
}
