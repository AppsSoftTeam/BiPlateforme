using System;
using System.Linq;
using System.Web.Security;

namespace TestApp.Models
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            using (ProjectContext db = new ProjectContext())
            {
                return db.Profiles.Select(r => r.ProfilName).ToArray();
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (ProjectContext db = new ProjectContext())
            {
                var userRoles = (from user in db.Users
                                 join roleMapping in db.User_Profils
                                 on user.UserId equals roleMapping.UserId
                                 join role in db.Profiles
                                 on roleMapping.ProfilId equals role.ProfilId
                                 where user.UserName == username
                                 select role.ProfilName).ToArray();
                return userRoles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}