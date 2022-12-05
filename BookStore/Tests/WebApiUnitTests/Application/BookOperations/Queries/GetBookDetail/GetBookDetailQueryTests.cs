using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Queries.GetBookDetail
{
	public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private	readonly IMapper _mapper;

		public GetBookDetailQueryTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			GetBookDetailQuery q = new GetBookDetailQuery(_context, null);

			FluentActions
			.Invoking(() => q.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_BookViewModel_ShouldBeReturn()
		{
			var book = new Book() {Title="Test Get Detail Book Model", AuthorId=1, GenreId=1, PageCount=1, PublishDate=DateTime.Now.Date.AddYears(-1)};
			_context.Books.Add(book);
			_context.SaveChanges();

			GetBookDetailQuery q = new GetBookDetailQuery(_context, _mapper);
			q.Id = book.Id;
			var mappedBook = _mapper.Map<BookViewModel>(_context.Books.Find(book.Id));

			FluentActions
			.Invoking(() => q.Handle()).Invoke().Should().NotBeNull();
		}	
	}
}
