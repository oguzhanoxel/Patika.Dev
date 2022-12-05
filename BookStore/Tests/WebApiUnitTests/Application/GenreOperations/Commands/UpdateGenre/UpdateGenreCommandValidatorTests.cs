using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Fact]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
		{
			// arrange
			UpdateGenreCommand command = new UpdateGenreCommand(null);

			command.Model = new UpdateGenreModel() {
				Name = "",
			};

			// act
			UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			// arrange
			UpdateGenreCommand command = new UpdateGenreCommand(null);

			command.Model = new UpdateGenreModel() {
				Name = "Update Test",
			};

			// act
			UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
