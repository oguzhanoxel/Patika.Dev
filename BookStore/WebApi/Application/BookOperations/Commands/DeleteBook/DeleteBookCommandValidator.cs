using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
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
