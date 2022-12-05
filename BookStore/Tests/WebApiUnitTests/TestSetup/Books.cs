using WebApi.DbAccess;
using WebApi.Entities;

namespace WebApiUnitTests.TestSetup
{
	public static class Books
	{
		public static void AddBooks(this BookStoreDbContext context)
		{

			context.Books.AddRange(
					new Book() { Title = "Call of Cthulhu", AuthorId = 3, GenreId = 1, PageCount = 200, PublishDate = new DateTime(2022, 12, 21) },
					new Book() { Title = "Time Machine", AuthorId = 2, GenreId = 2, PageCount = 220, PublishDate = new DateTime(2015, 12, 21) },
					new Book() { Title = "Attack on Titan", AuthorId = 4, GenreId = 3, PageCount = 300, PublishDate = new DateTime(2002, 12, 21) },
					new Book() { Title = "Herland", AuthorId = 1, GenreId = 1, PageCount = 100, PublishDate = new DateTime(2011, 12, 21) }
				);
		}
	}
}
