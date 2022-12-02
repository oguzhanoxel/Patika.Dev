using AutoMapper;
using WebApi.DbAccess;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQuery
	{
		public int Id { get; set; }
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public AuthorDetailModel Handle()
		{
			var author = _context.Authors.SingleOrDefault(a => a.Id == Id);
			if(author is null)
			{
				throw new InvalidOperationException("Book not exist");
			}
			var mappedAuthor = _mapper.Map<AuthorDetailModel>(author);
			return mappedAuthor;
		}

	}
	public class AuthorDetailModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
