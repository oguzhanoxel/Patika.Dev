using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.UpdateBook
{
	public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public UpdateBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);

			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not exist");
		}

		[Fact]
		public void WhenAlreadtExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			// arrange
			var book = new Book() {Title = "Update Book Test", AuthorId=1, GenreId=1, PageCount=1, PublishDate=DateTime.Now.Date.AddYears(-1)};
			_context.Books.Add(book);
			_context.SaveChanges();
			
			UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
			UpdateBookModel model = new UpdateBookModel() {
				Title="Update Book Test",
			};
			command.Id = _context.Books.First().Id;
			command.Model = model;

			// act & assert
			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book title exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Book_ShouldBeUpdated()
		{
			// arrange
			var book = new Book() {Title = "Update Book Test", AuthorId=1, GenreId=1, PageCount=1, PublishDate=DateTime.Now.Date.AddYears(-1)};
			_context.Books.Add(book);
			_context.SaveChanges();

			UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
			UpdateBookModel model = new UpdateBookModel()
			{
				Title="Updated Book Test",
				AuthorId=1,
				GenreId=1,
				PageCount=1,
				PublishDate= DateTime.Now.Date.AddYears(-1),
			};
			command.Id = _context.Books.Last().Id;
			command.Model = model;

			// act
			FluentActions.Invoking(() => command.Handle()).Invoke();

			// assert
			book = _context.Books.Single(b => b.Id == command.Id);
			book.Should().NotBeNull();
			book.Title.Should().Be(model.Title);
			book.AuthorId.Should().Be(model.AuthorId);
			book.GenreId.Should().Be(model.GenreId);
			book.PageCount.Should().Be(model.PageCount);
			book.PublishDate.Should().Be(model.PublishDate);
		}
	}
}
