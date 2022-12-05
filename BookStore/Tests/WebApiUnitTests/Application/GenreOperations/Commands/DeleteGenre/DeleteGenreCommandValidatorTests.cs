using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Commands.CreateGenre
{
	public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Fact]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
		{
			// arrange
			DeleteGenreCommand command = new DeleteGenreCommand(null);
			command.Id = -1;

			// act
			DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			// arrange
			DeleteGenreCommand command = new DeleteGenreCommand(null);
			command.Id = 1;
			
			// act
			DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
