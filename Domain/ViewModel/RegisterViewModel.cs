using Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
   public class RegisterViewModel
    {
       public List<UserViewModel>? Users { get; set; }
        public NewRegister NewUser { get; set; }
        public List<IdentityRole> Roles { get; set; }=new List<IdentityRole>();
        public ChangePasswordViewModel ChangePassword { get; set; }


    }
    public class NewRegister{
        public string? Id { get; set; }
        [Required(ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName ="UserName")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MinLength")]

        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "UserEmail")]
        [EmailAddress( ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "UserEmailError")]
        [MaxLength(20,ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MaxLength")]

        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MinLength")]
        public string Email { get; set; }
      
        public string? ImageUser { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "RoleName")]

        public string RoleName { get; set; }
        public bool ActiveUser { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "Password")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MinLength")]

        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "ComparePassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "ComparePasswordError")]
        public string ComparePassword { get; set; }
    }
}
