using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
	public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
	{
		public UpdateBookCommandValidator()
		{
			RuleFor(c => c.Id).GreaterThan(0);
			RuleFor(c => c.Model.GenreId).GreaterThan(0);
			RuleFor(c => c.Model.PageCount).GreaterThan(0);
			RuleFor(c => c.Model.PublishDate.Date).NotEmpty();
			RuleFor(c => c.Model.Title).MinimumLength(1);
		}
	}
}
