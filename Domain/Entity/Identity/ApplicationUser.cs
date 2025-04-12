using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Identity
{
   
    public class ApplicationUser:IdentityUser
	{
    
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public bool ActiveUser { get; set; }
    }
}
