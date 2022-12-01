using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
	public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
	{
		public UpdateBookCommandValidator()
		{
			RuleFor(c => c.Id).GreaterThan(0);
			RuleFor(c => c.Model.GenreId).GreaterThanOrEqualTo(0);
			RuleFor(c => c.Model.PageCount).GreaterThanOrEqualTo(0);
			RuleFor(c => c.Model.PublishDate.Date).NotEmpty();
			RuleFor(c => c.Model.Title).MinimumLength(1);
		}
	}
}
