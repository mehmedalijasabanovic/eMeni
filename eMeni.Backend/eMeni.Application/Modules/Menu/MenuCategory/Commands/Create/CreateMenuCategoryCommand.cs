using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Create
{
    public sealed class CreateMenuCategoryCommand:IRequest<int>
    {
        public int MenuId { get; set; }
        public string CategoryName { get; set; }
        public int OrderIndex { get; set; }
        public string Description { get; set; }
    }
}
