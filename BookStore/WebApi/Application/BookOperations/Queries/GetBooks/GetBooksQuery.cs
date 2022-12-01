using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbAccess;

namespace WebApi.Application.BookOperations.Quaries.GetBooks
{
	public class GetBooksQuery
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<BooksViewModel> Handle()
		{
			var bookList = _context.Books.Include(b => b.Genre).OrderBy(b => b.Id).ToList();
			List<BooksViewModel> mappedList = _mapper.Map<List<BooksViewModel>>(bookList);
			return mappedList;
		}
	}

	public class BooksViewModel
	{
		public string Title { get; set; }
		public int PageCount { get; set; }
		public string PublishDate { get; set; }
		public string Genre { get; set; }
	}
}
