using Microsoft.EntityFrameworkCore;

namespace WebApi.DbAccess
{

	public class BookStoreDbContext : DbContext {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "BookStoreDb");
		}
		public DbSet<Book> Books { get; set; }
	}
}
