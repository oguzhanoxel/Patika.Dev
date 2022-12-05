using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.UpdateBook
{
	public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
		[InlineData("", 0, 0, 0)]
		[InlineData("", 1, 1, 1)]
		[InlineData("Test", 0, 1, 1)]
		[InlineData("Test", 1, 0, 1)]
		[InlineData("Test", 1, 1, 0)]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
		{
			// arrange
			UpdateBookCommand command = new UpdateBookCommand(null, null);
			command.Model = new UpdateBookModel() {
				Title=title,
				PageCount=pageCount,
				GenreId=genreId,
				PublishDate=DateTime.Now.Date.AddYears(-1),
			};

			// act
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
		{
			UpdateBookCommand command = new UpdateBookCommand(null, null);
			command.Model = new UpdateBookModel() {
				Title="Test",
				PageCount=1,
				GenreId=1,
				PublishDate=DateTime.Now.Date,
			};
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			UpdateBookCommand command = new UpdateBookCommand(null, null);
			command.Id = 3;
			command.Model = new UpdateBookModel() {
				Title="UpdateTest",
				PageCount=1,
				GenreId=1,
				AuthorId=1,
				PublishDate=DateTime.Now.Date.AddYears(-1),
			};
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
