using FluentValidation;

namespace WebApi.BookOperations.GetBookById
{
	public class GetBookByIdQueryValidator : AbstractValidator<GetBookById>
	{
		public GetBookByIdQueryValidator()
		{
			RuleFor(d => d.Id).GreaterThan(0);
		}
	}
}
