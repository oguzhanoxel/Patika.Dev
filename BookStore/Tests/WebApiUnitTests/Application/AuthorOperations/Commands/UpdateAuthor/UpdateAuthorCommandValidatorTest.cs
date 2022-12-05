using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name, string surname)
		{
			UpdateAuthorCommand command = new UpdateAuthorCommand(null);

			UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			UpdateAuthorCommand command = new UpdateAuthorCommand(null);
			command.Id = 1;
			UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
