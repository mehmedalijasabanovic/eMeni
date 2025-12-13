using eMeni.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Common.Services
{
    public class AuthorizationHelper : IAuthorizationHelper
    {
        private readonly IAppCurrentUser _user;

        public AuthorizationHelper(IAppCurrentUser user)
        {
            _user = user;
        }
        public void EnsureAuthenticated()
        {
            if (!_user.IsAuthenticated)
                throw new eMeniBusinessRuleException(
                    Messages.NotAuthenticatedCode,
                    Messages.NotAuthenticated
                );
        }

        public void EnsureAdmin()
        {
            EnsureAuthenticated();

            if (!_user.IsAdmin)
                throw new eMeniBusinessRuleException(
                    Messages.NotAuthorizedCode,
                    Messages.NotAuthorized
                );
        }

        public void EnsureOwner()
        {
            EnsureAuthenticated();

            if (!_user.IsOwner)
                throw new eMeniBusinessRuleException(
                    Messages.NotAuthorizedCode,
                    Messages.NotAuthorized
                );
        }
    }
}
