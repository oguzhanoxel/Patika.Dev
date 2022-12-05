using WebApi.DbAccess;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommand
	{
		public int Id { get; set; }
		public readonly IBookStoreDbContext _context;

		public DeleteGenreCommand(IBookStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var genre = _context.Genres.Find(Id);
			if(genre is null)
			{
				throw new InvalidOperationException("Genre not exist");
			}
			_context.Genres.Remove(genre);
			_context.SaveChanges();
		}
	}
}
