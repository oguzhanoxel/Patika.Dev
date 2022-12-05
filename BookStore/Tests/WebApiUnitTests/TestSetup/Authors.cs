using WebApi.DbAccess;
using WebApi.Entities;

namespace WebApiUnitTests.TestSetup
{
	public static class Authors
	{
		public static void AddAuthors(this BookStoreDbContext context)
		{
			context.AddRange(
				new Author() { Name = "Charlotte Perkins", Surname = "Gilman" },
				new Author() { Name = "Herbert George", Surname = "Wells" },
				new Author() { Name = "Howard Phillips", Surname = "Lovecraft" },
				new Author() { Name = "Hajime", Surname = "Isayama" }
			);
		}
	}
}
