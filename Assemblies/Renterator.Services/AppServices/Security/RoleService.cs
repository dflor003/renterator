using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Renterator.Common;
using Renterator.DataAccess.Infrastructure;
using Renterator.DataAccess.Model;

namespace Renterator.Services.AppServices.Security
{
    public class RoleService : RoleProvider
    {
        private readonly IDataAccessor dataAccessor;

        public RoleService(IDataAccessor dataAccessor)
        {
            this.dataAccessor = Utils.NullArgumentCheck("dataAccessor", dataAccessor);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return
                (from user in dataAccessor.Users.Include(x => x.Roles)
                 where
                     user.Username == username &&
                     user.Roles.Any(x => x.RoleName == roleName)
                 select 1).Any();

        }

        public override string[] GetRolesForUser(string username)
        {
            IEnumerable<string> roles =
                (from user in dataAccessor.Users.Include(x => x.Roles)
                 where user.Username == username
                 select user.Roles.Select(x => x.RoleName))
                .FirstOrDefault();

            return roles == null ? new string[0] : roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            Role role = new Role { RoleName = roleName };
            dataAccessor.Create(role);
            dataAccessor.SaveChanges();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            // Find matching role
            var matchingRole =
                (from role in dataAccessor.Roles.Include(x => x.Users)
                 where role.RoleName == roleName
                 select new { role.Id, role.RoleName, UserCount = role.Users.Count })
                .SingleOrDefault();

            // Throw error if no matching role
            if (matchingRole == null)
            {
                throw new FormattedException("Delete Role: Invalid role '{0}'", roleName);
            }

            // Throw error if populated
            if (throwOnPopulatedRole && matchingRole.UserCount > 0)
            {
                throw new FormattedException("Delete Role: Role still has {0} user(s) assigned", matchingRole.UserCount);
            }

            // Delete role
            dataAccessor.Delete<Role>(x => x.Id == matchingRole.Id);
            return dataAccessor.SaveChanges() > 0;
        }

        public override bool RoleExists(string roleName)
        {
            return dataAccessor.Roles.Any(x => x.RoleName == roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var roleUserMappings =
                from role in dataAccessor.Roles
                where roleNames.Contains(role.RoleName)
                select new
                {
                    Role = role,
                    Users =
                        from user in dataAccessor.Users
                        where usernames.Contains(user.Username)
                        select user
                };

            foreach (var mapping in roleUserMappings)
            {
                foreach (var user in mapping.Users)
                {
                    mapping.Role.Users.Add(user);
                }
            }

            dataAccessor.SaveChanges();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var roleUserMappings =
                from role in dataAccessor.Roles
                where roleNames.Contains(role.RoleName)
                select new
                {
                    Role = role,
                    Users =
                        from user in dataAccessor.Users
                        where usernames.Contains(user.Username)
                        select user
                };

            foreach (var mapping in roleUserMappings)
            {
                foreach (var user in mapping.Users)
                {
                    mapping.Role.Users.Remove(user);
                }
            }

            dataAccessor.SaveChanges();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return
                (from role in dataAccessor.Roles
                 where role.RoleName == roleName
                 from user in role.Users
                 select user.Username)
                .ToArray();
        }

        public override string[] GetAllRoles()
        {
            return
                (from role in dataAccessor.Roles
                 select role.RoleName)
                .ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return
                (from role in dataAccessor.Roles
                 where role.RoleName == roleName
                 from user in role.Users
                 where user.Username.Contains(usernameToMatch)
                 select user.Username)
                .ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}
