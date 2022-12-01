using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
	{
		public DeleteGenreCommandValidator()
		{
			RuleFor(g => g.Id).NotEmpty().GreaterThanOrEqualTo(0);
		}
	}
}
