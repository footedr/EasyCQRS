using EasyCQRS.Web.Features.Employees;
using Microsoft.EntityFrameworkCore;

namespace EasyCQRS.Web.Data
{
	public class EasyCqrsContext : DbContext
	{
		public EasyCqrsContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>(employee => 
				employee.Property(emp => emp.Name)
					.IsRequired()
			);				
			base.OnModelCreating(modelBuilder);
		}
	}
}