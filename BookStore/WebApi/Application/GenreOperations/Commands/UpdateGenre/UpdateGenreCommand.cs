using WebApi.DbAccess;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
		public class UpdateGenreCommand
	{
		public int Id { get; set; }
		public UpdateGenreModel Model { get; set; }
		public readonly BookStoreDbContext _context;

		public UpdateGenreCommand(BookStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var genre = _context.Genres.FirstOrDefault(g => g.Id == Id);
			if(genre is null)
			{
				throw new InvalidOperationException("Genre not exist.");
			}
			if(_context.Genres.Any(g => g.Name.ToLower() == Model.Name.ToLower() && g.Id != Id))
			{
				throw new InvalidOperationException("Genre name exist.");
			}
			genre.Name = Model.Name.Trim() != default ? Model.Name : genre.Name;
			genre.IsActive = Model.IsActive != default ? Model.IsActive : genre.IsActive;
			_context.SaveChanges();
		}
	}

	public class UpdateGenreModel
	{
		public string Name { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
