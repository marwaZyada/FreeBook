using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<LogCategory> LogCategories { get; set; } = new List<LogCategory>();
        public CategoryView NewCategory { get; set; }
    }
    public class CategoryView
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("CategoryName"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "MinLength")]

        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentState { get; set; }
    }

    }
