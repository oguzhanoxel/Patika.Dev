using WebApi.DbAccess;

namespace WebApi.BookOperations.UpdateBook
{
	public class UpdateBookCommand
	{
		public int Id{ get; set; }
		public UpdateBookModel Model { get; set; }
		private readonly BookStoreDbContext _context;

		public UpdateBookCommand(BookStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var book = _context.Books.Find(Id);
			if (book is null)
			{
				throw new InvalidOperationException("Book not exist.");
			}
			book.Title = Model.Title != default ? Model.Title : book.Title;
			book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
			book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
			book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
			_context.SaveChanges();
		}
	}
	public class UpdateBookModel
	{
		public string Title { get; set; }
		public int GenreId { get; set; }
		public int PageCount { get; set; }
		public DateTime PublishDate { get; set; }
	}
}
