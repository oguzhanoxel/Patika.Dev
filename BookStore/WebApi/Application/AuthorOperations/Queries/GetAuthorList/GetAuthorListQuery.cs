using System.Linq;
using AutoMapper;
using WebApi.DbAccess;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorList
{
	public class GetAuthorListQuery
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetAuthorListQuery(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<AuthorModel> Handle()
		{
			var authorList = _context.Authors.OrderBy(a => a.Id);
			var mappedAuthorList = _mapper.Map<List<AuthorModel>>(authorList);
			return mappedAuthorList;
		}
	}

	public class AuthorModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
