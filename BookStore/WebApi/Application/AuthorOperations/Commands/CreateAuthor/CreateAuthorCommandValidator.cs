using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
	{
		public CreateAuthorCommandValidator()
		{
			RuleFor(a => a.Model.Name).NotNull().MinimumLength(1);
			RuleFor(a => a.Model.Surname).NotNull().MinimumLength(1);
			RuleFor(a => a.Model.DateOfBirth).NotNull();
		}
	}
}
