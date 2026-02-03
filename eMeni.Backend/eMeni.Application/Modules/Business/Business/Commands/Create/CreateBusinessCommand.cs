using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Business.Commands.Create
{
    public sealed class CreateBusinessCommand:IRequest<int>
    {
        public string BusinessName { get; set; }
        public int BusinessCategoryId { get; set; }
        public int BusinessProfileId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }

    }
}
