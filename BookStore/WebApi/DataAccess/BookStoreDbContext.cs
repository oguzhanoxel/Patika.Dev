using Microsoft.EntityFrameworkCore;

namespace WebApi.DbAccess
{

	public class BookStoreDbContext : DbContext {
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
		{
			
		}

		public DbSet<Book> Books { get; set; }
	}
	
}
