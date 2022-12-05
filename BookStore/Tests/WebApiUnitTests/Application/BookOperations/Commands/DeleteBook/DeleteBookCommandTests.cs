using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.DeleteBook
{
	public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;

		public DeleteBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Fact]
		public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			DeleteBookCommand command = new DeleteBookCommand(_context);

			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
		{
			var book = new Book() {Title = "Delete Book Test", AuthorId=1, GenreId=1, PageCount=1, PublishDate=DateTime.Now.Date.AddYears(-1)};
			_context.Books.Add(book);
			_context.SaveChanges();

			DeleteBookCommand command = new DeleteBookCommand(_context);
			command.Id = book.Id;

			FluentActions.Invoking(() => command.Handle()).Invoke();

			book = _context.Books.SingleOrDefault(b => b.Id == book.Id);
			book.Should().BeNull();
		}
	}
}
