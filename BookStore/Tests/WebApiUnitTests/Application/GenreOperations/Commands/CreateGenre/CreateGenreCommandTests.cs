using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Commands.CreateGenre
{
	public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;

		public CreateGenreCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Fact]
		public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			// arrange
			var genre = new Genre() {
				Name="Create Genre",
			};
			_context.Genres.Add(genre);
			_context.SaveChanges();
			
			CreateGenreCommand command = new CreateGenreCommand(_context);
			command.Model = new CreateGenreModel() { Name = genre.Name };;
		

			// act & assert
			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
		{
			// arrange
			CreateGenreCommand command = new CreateGenreCommand(_context);
			CreateGenreModel model = new CreateGenreModel()
			{
				Name="Create Genre Test",
			};
			command.Model = model;

			// act
			FluentActions.Invoking(() => command.Handle()).Invoke();

			// assert
			var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
			genre.Should().NotBeNull();
		}
	}
}
