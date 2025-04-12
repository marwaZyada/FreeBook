using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Book.Permission
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallBackPolicyProvider { get;  }
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallBackPolicyProvider=new DefaultAuthorizationPolicyProvider(options);
        }
        public async Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
          return  await FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public async Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return await FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(Helper.Permission, System.StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequiredment(policyName));
                return Task.FromResult(policy.Build()); 
            }
            return FallBackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
