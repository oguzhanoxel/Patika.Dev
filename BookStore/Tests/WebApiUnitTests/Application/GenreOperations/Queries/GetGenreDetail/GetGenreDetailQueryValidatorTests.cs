using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Fact]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
		{
			GetGenreDetailQuery q = new GetGenreDetailQuery(null, null);
			q.Id = -1;

			GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
			var result = validator.Validate(q);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			GetGenreDetailQuery q = new GetGenreDetailQuery(null, null);
			q.Id = 1;

			GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
			var result = validator.Validate(q);
			result.Errors.Count.Should().Be(0);
		}
	}
}
