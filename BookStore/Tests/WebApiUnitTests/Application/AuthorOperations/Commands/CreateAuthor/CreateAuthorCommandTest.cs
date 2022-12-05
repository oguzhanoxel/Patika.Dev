using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public CreateAuthorCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenAlreadyExistAuthorNameAndSurnameAreGiven_InvalidOperationException_ShouldBeReturn()
		{
			var author = new Author() {Name="TestName", Surname="TestSurname", DateOfBirth=DateTime.Now.Date.AddYears(-1)};
			_context.Authors.Add(author);
			_context.SaveChanges();

			CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
			command.Model = new CreateAuthorModel() {Name=author.Name, Surname=author.Surname, DateOfBirth=author.DateOfBirth};

			FluentActions
			.Invoking(() => command.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author Exist");
		}

		[Fact]
		public void WhenValidInputAreGiven_Author_ShouldBeCreated()
		{
			CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
			CreateAuthorModel model = new CreateAuthorModel()
			{
				Name="Test Name",
				Surname="Test Surname",
				DateOfBirth=DateTime.Now.Date.AddYears(-1)
			};
			command.Model = model;

			FluentActions.Invoking(() => command.Handle()).Invoke();

			var author = _context.Authors.Single(author => author.Name == model.Name);
			author.Should().NotBeNull();
			author.Surname.Should().Be(model.Surname);
			author.DateOfBirth.Should().Be(model.DateOfBirth);
		}
	}
}
