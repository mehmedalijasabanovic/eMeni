using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Delete
{
    public sealed class DeleteMenuCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
