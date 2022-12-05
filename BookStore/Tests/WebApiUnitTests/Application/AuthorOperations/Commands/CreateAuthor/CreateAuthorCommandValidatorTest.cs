using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
		[InlineData("", "")]
		[InlineData("", "test")]
		[InlineData("test", "")]
		[InlineData(null, null)]
		[InlineData(null, "test")]
		[InlineData("test", null)]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name, string surname)
		{
			CreateAuthorCommand command = new CreateAuthorCommand(null, null);
			command.Model = new CreateAuthorModel() {
				Name=name,
				Surname=surname,
				DateOfBirth=DateTime.Now.Date.AddYears(-1)
			};

			CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			CreateAuthorCommand command = new CreateAuthorCommand(null, null);
			command.Model = new CreateAuthorModel() {
				Name="Test Name",
				Surname="Test Surname",
				DateOfBirth=DateTime.Now.Date.AddYears(-1)
			};
			CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
			var result = validator.Validate(command);
			result.Errors.Count.Should().Be(0);
		}
	}
}
