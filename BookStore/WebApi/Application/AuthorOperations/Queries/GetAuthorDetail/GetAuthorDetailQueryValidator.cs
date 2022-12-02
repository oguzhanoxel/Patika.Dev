using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
	{
		public GetAuthorDetailQueryValidator()
		{
			RuleFor(a => a.Id).NotNull().GreaterThanOrEqualTo(0);
		}
	}
}
