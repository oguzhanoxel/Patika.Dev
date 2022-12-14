using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbAccess;
using WebApi.Mapping;

namespace WebApiUnitTests.TestSetup
{
	public class CommonTestFixture
	{
		public BookStoreDbContext Context { get; set; }
		public IMapper Mapper { get; set; }

		public CommonTestFixture()
		{
			var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDb").Options;
			Context = new BookStoreDbContext(options);
			
			Context.Database.EnsureCreated();
			Context.AddAuthors();
			Context.AddGenres();
			Context.AddBooks();
			Context.SaveChanges();

			Mapper = new MapperConfiguration(cfg => {
				cfg.AddProfile<MappingProfile>();
			}).CreateMapper();
		}
	}
}
