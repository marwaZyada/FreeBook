using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
   public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "Password")]

        public string NewPassword { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "ComparePassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "ComparePasswordError")]
        public string ComparePassword { get; set; }
    }
}
