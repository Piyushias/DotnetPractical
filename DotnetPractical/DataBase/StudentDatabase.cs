using DotnetPractical.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetPractical.DataBase
{
	public class StudentDatabase:DbContext
	{
		public StudentDatabase(DbContextOptions<StudentDatabase> options) : base(options) { }

		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Subject> Subjects { get; set; }


		


	}
}
