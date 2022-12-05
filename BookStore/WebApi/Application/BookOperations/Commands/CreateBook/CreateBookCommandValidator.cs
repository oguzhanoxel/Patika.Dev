using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
	public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
	{
		public CreateBookCommandValidator()
		{
			RuleFor(c => c.Model.Title).NotNull().MinimumLength(1);
			RuleFor(c => c.Model.AuthorId).NotNull().GreaterThan(0);
			RuleFor(c => c.Model.GenreId).NotNull().GreaterThan(0);
			RuleFor(c => c.Model.PageCount).NotNull().GreaterThan(0);
			RuleFor(c => c.Model.PublishDate.Date).NotNull().LessThan(DateTime.Now.Date);
		}
	}
}
