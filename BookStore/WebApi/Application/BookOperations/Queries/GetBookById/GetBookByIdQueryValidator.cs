using FluentValidation;

namespace WebApi.Application.BookOperations.Quaries.GetBookById
{
	public class GetBookByIdQueryValidator : AbstractValidator<GetBookById>
	{
		public GetBookByIdQueryValidator()
		{
			RuleFor(d => d.Id).GreaterThan(0);
		}
	}
}
