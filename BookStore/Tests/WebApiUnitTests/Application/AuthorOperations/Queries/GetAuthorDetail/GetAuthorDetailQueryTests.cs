using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private	readonly IMapper _mapper;

		public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			GetAuthorDetailQuery q = new GetAuthorDetailQuery(_context, null);

			FluentActions
			.Invoking(() => q.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_AuthorViewModel_ShouldBeReturn()
		{
			var author = new Author() {Name="Test Author", Surname="Test Author Surname", DateOfBirth=DateTime.Now.Date.AddYears(-1)};
			_context.Authors.Add(author);
			_context.SaveChanges();
			GetAuthorDetailQuery q = new GetAuthorDetailQuery(_context, _mapper);
			q.Id = author.Id;
			var mappedAuthor = _mapper.Map<AuthorDetailModel>(_context.Authors.Find(author.Id));

			var returnAuthor = q.Handle();
			returnAuthor.Should().NotBeNull();
			returnAuthor.Name.Should().Be(mappedAuthor.Name);
			returnAuthor.Surname.Should().Be(mappedAuthor.Surname);
			returnAuthor.DateOfBirth.Should().Be(mappedAuthor.DateOfBirth);
		}	
	}
}
