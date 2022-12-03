using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Commands.CreateBook
{
	public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{

		[Theory]
		[InlineData("Test Title", 0, 0)]
		[InlineData("Test Title", 1, 0)]
		[InlineData("Test Title", 0, 1)]
		[InlineData("", 0, 0)]
		[InlineData("", 1, 0)]
		[InlineData("", 0, 1)]
		[InlineData("", 1, 1)]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
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
				PublishDate=DateTime.Now.Date.AddYears(-1),
			};
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
