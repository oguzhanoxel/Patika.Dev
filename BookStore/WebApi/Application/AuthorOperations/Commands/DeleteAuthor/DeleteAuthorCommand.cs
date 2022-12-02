using System.Linq;
using WebApi.DbAccess;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
	public class DeleteAuthorCommand
	{
		public int Id { get; set; }
		private readonly BookStoreDbContext _context;

		public DeleteAuthorCommand(BookStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var author = _context.Authors.SingleOrDefault(a => a.Id == Id);
			if(author is null)
			{
				throw new InvalidOperationException("Author not exist");
			}
			if(_context.Books.Any(b => b.AuthorId == Id))
			{
				throw new InvalidOperationException("Author's books need to be deleted");
			}
			_context.Authors.Remove(author);
			_context.SaveChanges();
		}
	}
}
