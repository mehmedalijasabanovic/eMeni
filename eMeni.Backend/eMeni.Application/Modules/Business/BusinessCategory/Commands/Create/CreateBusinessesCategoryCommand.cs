using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.BusinessCategory.Commands.Create
{
    public class CreateBusinessesCategoryCommand : IRequest<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
