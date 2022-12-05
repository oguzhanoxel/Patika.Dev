using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbAccess;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.DeleteBook
{
	public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;

		public DeleteBookCommandValidatorTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(null)]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
		{
			DeleteBookCommand command = new DeleteBookCommand(null);
			command.Id = id;

			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			var result = validator.Validate(command);
			
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			DeleteBookCommand command = new DeleteBookCommand(null);
			command.Id = _context.Books.First().Id;

			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			var result = validator.Validate(command);
			
			result.Errors.Count.Should().Be(0);
		}
		
	}
}
