using Microsoft.AspNetCore.Authorization;

namespace Book.Permission
{
    public class PermissionRequiredment:IAuthorizationRequirement
    {
        public string Permission { get;private set; }
        public PermissionRequiredment(string permission)
        {
            Permission = permission;
            
        }
    }
}
