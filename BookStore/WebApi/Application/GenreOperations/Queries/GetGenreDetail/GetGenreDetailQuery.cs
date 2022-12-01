using AutoMapper;
using WebApi.DbAccess;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQuery
	{
		public int Id { get; set; }
		public readonly BookStoreDbContext _context;
		public readonly IMapper _mapper;

		public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public GenreDetailViewModel Handle()
		{
			var genre = _context.Genres.SingleOrDefault(g => g.IsActive && g.Id == Id);
			if (genre is null)
			{
				throw new InvalidOperationException("Book not found.");
			}
			GenreDetailViewModel mappedGenre = _mapper.Map<GenreDetailViewModel>(genre);
			return mappedGenre;
		}
	}

	public class GenreDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}