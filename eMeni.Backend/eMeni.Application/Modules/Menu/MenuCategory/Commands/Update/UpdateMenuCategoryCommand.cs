using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Update
{
    public sealed class UpdateMenuCategoryCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int OrderIndex { get; set; }
        public string Description { get; set; }
    }

}
