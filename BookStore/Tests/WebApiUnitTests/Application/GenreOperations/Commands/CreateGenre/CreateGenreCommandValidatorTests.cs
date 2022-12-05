using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbAccess;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Commands.CreateGenre
{
	public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Fact]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
		{
			// arrange
			CreateGenreCommand command = new CreateGenreCommand(null);
			command.Model = new CreateGenreModel() {
				Name="",
			};

			// act
			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			// arrange
			CreateGenreCommand command = new CreateGenreCommand(null);
			command.Model = new CreateGenreModel() {
				Name="Test",
			};

			// act
			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
