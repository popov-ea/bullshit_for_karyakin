using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace pisit.DataModel
{
	class AppDbContext : DbContext
	{
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

		public AppDbContext()
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=LAPTOP-NP3RP8H4; Database=pisitdb; Trusted_Connection=True");
		}
	}
}
