using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbAccess
{
	public interface IBookStoreDbContext
	{
		DbSet<Book> Books { get; set; }
		DbSet<Genre> Genres { get; set; }
		DbSet<Author> Authors { get; set; }

		int SaveChanges();

	}
}
