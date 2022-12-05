using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
	public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public DeleteAuthorCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenNonExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
			
			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not exist");
		}

		[Fact]
		public void WhenAuthorHasBooks_InvalidOperationException_ShouldBeReturn()
		{
			var book = new Book() {Title="DeleteTestBook", AuthorId=_context.Authors.First().Id, GenreId=1, PageCount=1, PublishDate=DateTime.Now.Date.AddYears(-1)};
			_context.Books.Add(book);
			_context.SaveChanges();

			DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
			command.Id = _context.Authors.First().Id;

			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author's books need to be deleted");
		}

		[Fact]
		public void WhenValidInputAreGiven_Author_ShouldBeDeleted()
		{
			var author = new Author()
			{
				Name="Test Author Name",
				Surname="Test Author Surname",
				DateOfBirth=DateTime.Now.Date.AddYears(-1)
			};
			_context.Authors.Add(author);
			_context.SaveChanges();

			DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
			command.Id = author.Id;

			FluentActions.Invoking(() => command.Handle()).Invoke();

			author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);
			author.Should().BeNull();
		}
	}
}
