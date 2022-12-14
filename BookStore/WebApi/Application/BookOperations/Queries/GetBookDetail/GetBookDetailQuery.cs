using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbAccess;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
	public class GetBookDetailQuery
	{
		public int Id { get; set ;}
		private readonly IBookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public BookViewModel Handle()
		{
			var book = _context.Books.Include(b => b.Genre).Include(b => b.Author).SingleOrDefault(b => b.Id == Id);
			if(book is null){
				throw new InvalidOperationException("Book not exist");
			}
			BookViewModel mappedBook = _mapper.Map<BookViewModel>(book);
			return mappedBook;
		}
	}

	public class BookViewModel{
		public string Title { get; set; }
		public string Author { get; set; }
		public int PageCount { get; set; }
		public string PublishDate { get; set; }
		public string Genre { get; set; }
	}
}
