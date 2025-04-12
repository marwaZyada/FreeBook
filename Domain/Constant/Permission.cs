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
    }
}
