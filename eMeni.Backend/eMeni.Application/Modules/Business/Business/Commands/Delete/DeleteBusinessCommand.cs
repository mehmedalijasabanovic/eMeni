using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Business.Commands.Delete
{
    public class DeleteBusinessCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
