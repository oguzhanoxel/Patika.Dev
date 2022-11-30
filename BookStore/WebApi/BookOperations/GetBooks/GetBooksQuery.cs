using WebApi.Common;
using WebApi.DbAccess;

namespace WebApi.BookOperations.GetBooks
{
	public class GetBooksQuery
	{
		private readonly BookStoreDbContext _context;

		public GetBooksQuery(BookStoreDbContext context)
		{
			_context = context;
		}

		public List<BooksViewModel> Handle()
		{
			var bookList = _context.Books.OrderBy(b => b.Id).ToList();
			List<BooksViewModel> viewModel = new List<BooksViewModel>();
			foreach (var book in bookList){
				viewModel.Add(new BooksViewModel() {
					Title = book.Title,
					Genre = ((GenreEnum)book.GenreId).ToString(),
					PublishDate = book.PublishDate.Date.ToString("dd/MM/yy"),
					PageCount = book.PageCount
				});
			}
			return viewModel;
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
