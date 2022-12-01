using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
	public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
	{
		public CreateBookCommandValidator()
		{
			RuleFor(c => c.Model.GenreId).GreaterThan(0);
			RuleFor(c => c.Model.PageCount).GreaterThan(0);
			RuleFor(c => c.Model.PublishDate.Date).NotEmpty();
			RuleFor(c => c.Model.Title).MinimumLength(1);
		}
	}
}
