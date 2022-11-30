using WebApi.DbAccess;

namespace WebApi.BookOperations.UpdateBook
{
	public class UpdateBookCommand
	{
		public int Id;
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
			book = new Book() {
				Id = this.Id,
				Title = Model.Title,
				PublishDate = Model.PublishDate,
				PageCount = Model.PageCount,
				GenreId = Model.GenreId
			};
			_context.Update(book);
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
