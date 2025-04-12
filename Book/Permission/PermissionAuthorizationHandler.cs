using Domain.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Book.Permission
{
    public class PermissionAuthorizationHandler:AuthorizationHandler<PermissionRequiredment>
    {
        public PermissionAuthorizationHandler()
        {
            
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context
            , PermissionRequiredment requirement)
        {
            if (context.User == null)
                return;
            var permission = context.User.Claims.Where(x=>x.Type==Helper.Permission&&
            x.Value==requirement.Permission&& x.Issuer=="LOCAL AUTHORITY");
            if (permission.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
