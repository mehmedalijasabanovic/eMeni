using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Business.Commands.Update
{
    public sealed class UpdateBusinessCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? BusinessName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}
