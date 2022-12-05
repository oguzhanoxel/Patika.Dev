using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.CreateBook
{
	public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public CreateBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			// arrange
			var book = new Book() {Title = "Test2", PageCount=100, GenreId=1, PublishDate= new DateTime(2012, 12, 1)};
			_context.Books.Add(book);
			_context.SaveChanges();
			
			CreateBookCommand command = new CreateBookCommand(_context, _mapper);
			command.Model = new CreateBookModel() { Title = book.Title };

			// act & assert
			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Book_ShouldBeCreated()
		{
			// arrange
			CreateBookCommand command = new CreateBookCommand(_context, _mapper);
			CreateBookModel model = new CreateBookModel()
			{
				Title="Test1",
				AuthorId=1,
				GenreId=1,
				PageCount=1,
				PublishDate= DateTime.Now.Date.AddYears(-1),
			};
			command.Model = model;

			// act
			FluentActions.Invoking(() => command.Handle()).Invoke();

			// assert
			var book = _context.Books.Single(book => book.Title == model.Title);
			book.Should().NotBeNull();
			book.PageCount.Should().Be(model.PageCount);
			book.GenreId.Should().Be(model.GenreId);
			book.AuthorId.Should().Be(model.AuthorId);
			book.PublishDate.Should().Be(model.PublishDate);
		}
	}
}
