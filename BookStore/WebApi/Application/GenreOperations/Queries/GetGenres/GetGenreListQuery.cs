using AutoMapper;
using WebApi.DbAccess;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
	public class GetGenreListQuery
	{
		public readonly IBookStoreDbContext _context;
		public readonly IMapper _mapper;

		public GetGenreListQuery(IBookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<GenreViewModel> Handle()
		{
			var genres = _context.Genres.Where(g => g.IsActive).OrderBy(g => g.Id);
			List<GenreViewModel> mappedGenres = _mapper.Map<List<GenreViewModel>>(genres);
			return mappedGenres;
		}
	}

	public class GenreViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
