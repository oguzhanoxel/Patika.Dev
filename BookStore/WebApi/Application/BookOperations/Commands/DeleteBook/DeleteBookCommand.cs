using WebApi.DbAccess;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
	public class DeleteBookCommand
	{
		public int Id { get; set; }
		private readonly IBookStoreDbContext _context;

		public DeleteBookCommand(IBookStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var book = _context.Books.SingleOrDefault(b => b.Id == Id);
			if (book is null)
			{
				throw new InvalidOperationException("Book not exist");
			}

			_context.Books.Remove(book);
			_context.SaveChanges();
		}
	}
}
