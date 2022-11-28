using Microsoft.EntityFrameworkCore;

namespace WebApi.DbAccess
{
	public class EfBookDal
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
			{
				if (context.Books.Any())
				{
					return;
				}

				context.Books.AddRange(
					new Book { Id = 1, Title = "Time Machine", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2021, 10, 11) },
					new Book { Id = 2, Title = "Herland", GenreId = 1, PageCount = 220, PublishDate = new DateTime(2011, 10, 22) },
					new Book { Id = 3, Title = "Call of Cthulhu", GenreId = 3, PageCount = 110, PublishDate = new DateTime(2001, 10, 11) }
				);

				context.SaveChanges();
			};
		}
	}
}
