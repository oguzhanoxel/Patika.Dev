using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
	public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
	{
		public DeleteBookCommandValidator()
		{
			RuleFor(x => x.Id)
			.NotNull()
			.GreaterThan(0);
		}
	}
}
