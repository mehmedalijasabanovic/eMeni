using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Location.Commands.Create
{
    public class CreateCityCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
