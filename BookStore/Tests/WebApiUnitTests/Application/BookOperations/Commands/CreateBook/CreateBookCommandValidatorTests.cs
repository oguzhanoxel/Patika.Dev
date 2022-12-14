using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.CreateBook
{
	public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{

		[Theory]
		[InlineData("", 0, 0, 0)]
		[InlineData("", 1, 1, 1)]
		[InlineData("Test", 0, 1, 1)]
		[InlineData("Test", 1, 0, 1)]
		[InlineData("Test", 1, 1, 0)]
		[InlineData(null, 1, 1, 1)]
		[InlineData("Test", null, 1, 1)]
		[InlineData("Test", 1, null, 1)]
		[InlineData("Test", 1, 1, null)]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
		{
			// arrange
			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel() {
				Title=title,
				PageCount=pageCount,
				GenreId=genreId,
				PublishDate=DateTime.Now.Date.AddYears(-1),
			};

			// act
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
		{
			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel() {
				Title="Test",
				PageCount=1,
				GenreId=1,
				PublishDate=DateTime.Now.Date,
			};
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel() {
				Title="Test",
				PageCount=1,
				GenreId=1,
				AuthorId=1,
				PublishDate=DateTime.Now.Date.AddYears(-1),
			};
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
