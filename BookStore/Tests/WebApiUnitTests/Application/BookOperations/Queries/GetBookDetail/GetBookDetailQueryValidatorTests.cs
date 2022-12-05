using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbAccess;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.BookOperations.Queries.GetBookDetail
{
	public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Fact]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
		{
			// arrange
			GetBookDetailQuery q = new GetBookDetailQuery(null, null);
			q.Id = -1;

			// act
			GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
			var result = validator.Validate(q);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			GetBookDetailQuery q = new GetBookDetailQuery(null, null);
			q.Id = 1;
			GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
			var result = validator.Validate(q);
			result.Errors.Count.Should().Be(0);
		}
	}
}
