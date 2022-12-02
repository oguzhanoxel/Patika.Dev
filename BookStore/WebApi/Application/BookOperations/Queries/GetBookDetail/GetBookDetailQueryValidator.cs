using FluentValidation;

namespace WebApi.Application.BookOperations.Quaries.GetBookDetail
{
	public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
	{
		public GetBookDetailQueryValidator()
		{
			RuleFor(d => d.Id).GreaterThan(0);
		}
	}
}
