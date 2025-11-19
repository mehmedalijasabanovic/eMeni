using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Location.Commands.Delete
{
    public class DeleteCityCommand :IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
