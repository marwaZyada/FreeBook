﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class LogSubCategory
	{
		public Guid Id { get; set; }
		public string Action { get; set; }
		public DateTime Date { get; set; }
		public string UserId { get; set; }
		public Guid SubCategoryId { get; set; }
		[ForeignKey("SubCategoryId")]
		public SubCategory SubCategory { get; set; }
        
    }
}
