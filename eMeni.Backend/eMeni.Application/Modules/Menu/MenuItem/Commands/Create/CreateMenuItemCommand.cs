using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.MenuItem.Commands.Create
{
    public sealed class CreateMenuItemCommand:IRequest<int>
    {
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
