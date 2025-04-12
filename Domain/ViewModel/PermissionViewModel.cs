using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<RoleClaimsViewModel> RoleClaims { get; set; } = new List<RoleClaimsViewModel>();
    }
    public class RoleClaimsViewModel
    {
       
        public string Value { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
