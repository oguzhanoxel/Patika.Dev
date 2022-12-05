using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Fact]
		public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
		{
			// arrange
			GetAuthorDetailQuery q = new GetAuthorDetailQuery(null, null);
			q.Id = -1;

			// act
			GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
			var result = validator.Validate(q);
			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
		{
			GetAuthorDetailQuery q = new GetAuthorDetailQuery(null, null);
			q.Id = 1;
			GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
			var result = validator.Validate(q);
			result.Errors.Count.Should().Be(0);
		}
	}
}
