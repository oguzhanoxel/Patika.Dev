using WebApi.Common;
using WebApi.DbAccess;

namespace WebApi.BookOperations.GetBookById
{
	public class GetBookById
	{
		public int Id;
		private readonly BookStoreDbContext _context;

		public GetBookById(BookStoreDbContext context)
		{
			_context = context;
		}

		public BookViewModel Handle()
		{
			var book = _context.Books.Where(b => b.Id == Id).SingleOrDefault();
			BookViewModel mappedBook = new BookViewModel() {
				Title = book.Title,
				Genre = ((GenreEnum)book.GenreId).ToString(),
				PublishDate = book.PublishDate.Date.ToString("dd/MM/yy"),
				PageCount = book.PageCount
			};
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
