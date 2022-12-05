using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbAccess;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;

		public DeleteGenreCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Fact]
		public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			// arrange
			DeleteGenreCommand command = new DeleteGenreCommand(_context);

			// act & assert
			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Genre_ShouldBeDeleted()
		{
			// arrange
			DeleteGenreCommand command = new DeleteGenreCommand(_context);
			command.Id = _context.Genres.First().Id;

			// act
			FluentActions.Invoking(() => command.Handle()).Invoke();

			// assert
			var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.Id);
			genre.Should().BeNull();
		}
	}
}
