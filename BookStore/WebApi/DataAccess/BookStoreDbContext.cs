using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbAccess
{

	public class BookStoreDbContext : DbContext {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "BookStoreDb");
		}
		public DbSet<Book> Books { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Author> Authors { get; set; }
	}
}
