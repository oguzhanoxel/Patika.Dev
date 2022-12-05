using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public UpdateAuthorCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenNonExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Author_ShouldBeUpdated()
		{
			var author = new Author()
			{
				Name="Test Author Name",
				Surname="Test Author Surname",
				DateOfBirth=DateTime.Now.Date.AddYears(-1)
			};
			_context.Authors.Add(author);
			_context.SaveChanges();

			var model = new UpdateAuthorModel()
			{
				Name="Updated Test Author Name",
				Surname="Updated Test Author Surname",
				DateOfBirth=DateTime.Now.Date.AddYears(-2)
			};
			UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
			command.Id = author.Id;
			command.Model = model;

			FluentActions.Invoking(() => command.Handle()).Invoke();

			author = _context.Authors.Single(author => author.Name == model.Name);
			author.Should().NotBeNull();
			author.Surname.Should().Be(model.Surname);
			author.DateOfBirth.Should().Be(model.DateOfBirth);
		}
	}
}
