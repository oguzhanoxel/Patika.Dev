using WebApi.DbAccess;
using WebApi.Entities;

namespace WebApiUnitTests.TestSetup
{
	public static class Genres
	{
		public static void AddGenres(this BookStoreDbContext context)
		{
				context.Genres.AddRange(
						new Genre() { Name = "Horror" },
						new Genre() { Name = "Science Fiction" },
						new Genre() { Name = "Post-Apocalyptic" }
					);
		}
	}
}
