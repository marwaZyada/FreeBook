using Domain.Entity;
using Domain.Entity.Identity;


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class FreeBookDbContext:IdentityDbContext<ApplicationUser>
	{
        public FreeBookDbContext(DbContextOptions<FreeBookDbContext> options):base(options) 
        {
            
        }
		
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Category>().Property(c => c.Id).HasDefaultValueSql("(newid())");
			builder.Entity<LogCategory>().Property(c => c.Id).HasDefaultValueSql("(newid())");
			builder.Entity<SubCategory>().Property(c => c.Id).HasDefaultValueSql("(newid())");
			builder.Entity<LogSubCategory>().Property(c => c.Id).HasDefaultValueSql("(newid())");
			builder.Entity<Book>().Property(c => c.Id).HasDefaultValueSql("(newid())");
			builder.Entity<LogBook>().Property(c => c.Id).HasDefaultValueSql("(newid())");
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<LogCategory> LogCategories { get; set; }
		public DbSet<SubCategory> SubCategories { get; set; }
		public DbSet<LogSubCategory> LogSubCategories { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<LogBook> LogBooks { get; set; }
	}
}
