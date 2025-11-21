using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.BusinessCategory.Commands.Delete
{
    public class DeleteBusinessesCategoryCommand:IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
