using System;
using System.Linq;
using System.Security.Principal;
using Renterator.DataAccess.Model;

namespace Renterator.Services.Dto
{
    public class UserContext : IPrincipal
    {
        private readonly string[] roles;

        public UserContext(string token, User user)
        {
            this.roles = user.Roles.Select(role => role.RoleName).ToArray();
            this.Token = token;
            this.Identity = new GenericIdentity(token);

            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserId = user.Id;
        }

        public string Token { get; private set; }

        public int UserId { get; private set; }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; set; }

        public bool IsInRole(string role)
        {
            return roles.Any(r => string.Equals(r, role, StringComparison.OrdinalIgnoreCase));
        }

        public IIdentity Identity { get; private set; }
    }
}
