using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbAccess;

namespace WebApi.Application.BookOperations.Quaries.GetBookById
{
	public class GetBookById
	{
		public int Id { get; set ;}
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetBookById(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public BookViewModel Handle()
		{
			var book = _context.Books.Include(b => b.Genre).Where(b => b.Id == Id).SingleOrDefault();
			if(book is null){
				throw new InvalidOperationException("Book not exist.");
			}
			BookViewModel mappedBook = _mapper.Map<BookViewModel>(book);
			return mappedBook;
		}
	}

	public class BookViewModel{
		public string Title { get; set; }
		public int PageCount { get; set; }
		public string PublishDate { get; set; }
		public string Genre { get; set; }
	}
}
