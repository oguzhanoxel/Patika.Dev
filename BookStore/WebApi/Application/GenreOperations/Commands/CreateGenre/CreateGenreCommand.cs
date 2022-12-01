using AutoMapper;
using WebApi.DbAccess;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
	public class CreateGenreCommand
	{
		public CreateGenreModel Model { get; set; }
		public readonly BookStoreDbContext _context;

		public CreateGenreCommand(BookStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var genre = _context.Genres.SingleOrDefault(g => g.Name == Model.Name);
			if(genre is not null)
			{
				throw new InvalidOperationException("Genre exist.");
			}
			_context.Genres.Add(new Genre() {
				Name = Model.Name
			});
			_context.SaveChanges();
		}
	}

	public class CreateGenreModel
	{
		public string Name { get; set; }
	}
}
