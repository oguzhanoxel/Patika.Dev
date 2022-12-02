using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
	{
		public UpdateAuthorCommandValidator()
		{
			RuleFor(a => a.Id).NotNull().GreaterThan(0);
		}
	}
}
