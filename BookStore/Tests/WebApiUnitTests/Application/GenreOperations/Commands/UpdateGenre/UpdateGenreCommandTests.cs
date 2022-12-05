using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;

		public UpdateGenreCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Fact]
		public void WhenNonExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			UpdateGenreCommand command = new UpdateGenreCommand(_context);

			FluentActions.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not exist");
		}

		[Fact]
		public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			var genre = new Genre() { Name= "Test Genre" };
			_context.Genres.Add(genre);
			_context.SaveChanges();

			UpdateGenreCommand command = new UpdateGenreCommand(_context);
			command.Id = genre.Id;
			command.Model = new UpdateGenreModel() { Name="Horror" };

			FluentActions.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre name exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Book_ShouldBeUpdated()
		{
			var genre = new Genre() { Name="Not Updated Genre" };
			_context.Genres.Add(genre);
			_context.SaveChanges();

			UpdateGenreCommand command = new UpdateGenreCommand(_context);
			var model = new UpdateGenreModel() { Name="Updated Genre" };
			command.Id = genre.Id;
			command.Model = model;

			FluentActions.Invoking(() => command.Handle()).Invoke();

			genre = _context.Genres.Single(genre => genre.Id == command.Id);
			genre.Should().NotBeNull();
			genre.Name.Should().Be(model.Name);
		}
	}
}
