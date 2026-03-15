using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Commands.Create
{
    public sealed class CreateUserCommand:IRequest<int>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }
    }
}
