using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constant
{
    public static class Permission
    {
        public static List<string> GeneratePermissionsFromModules(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                 $"Permissions.{module}.Edit",
                  $"Permissions.{module}.Create",
                   $"Permissions.{module}.Delete",
            };
        }
        public static List<string> PermissionsList()
        {
            var allpermissions = new List<string>();

            foreach (var item in Enum.GetValues(typeof(Helper.PermissionModuleName)))
            {
                allpermissions.AddRange(GeneratePermissionsFromModules(item.ToString()));
            }
            return allpermissions;

        }
        public static class Home
        {
            public const string View = "Permissions.Home.View";
            public const string Edit = "Permissions.Home.Edit";
            public const string Create = "Permissions.Home.Create";
            public const string Delete = "Permissions.Home.Delete";
        }

        public static class Accounts
        {
            public const string View = "Permissions.Accounts.View";
            public const string Edit = "Permissions.Accounts.Edit";
            public const string Create = "Permissions.Accounts.Create";
            public const string Delete = "Permissions.Accounts.Delete";
        }
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Create = "Permissions.Roles.Create";
            public const string Delete = "Permissions.Roles.Delete";
        }
        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Create = "Permissions.Categories.Create";
            public const string Delete = "Permissions.Categories.Delete";
        }

        public static class Register
        {
            public const string View = "Permissions.Register.View";
            public const string Edit = "Permissions.Register.Edit";
            public const string Create = "Permissions.Register.Create";
            public const string Delete = "Permissions.Register.Delete";
        }
    }
}
