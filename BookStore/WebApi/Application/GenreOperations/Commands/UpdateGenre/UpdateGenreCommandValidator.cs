using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
	{
		public UpdateGenreCommandValidator()
		{
			RuleFor(g => g.Model.Name).MinimumLength(1).When(g => g.Model.Name.Trim() == string.Empty);
		}
	}
}
