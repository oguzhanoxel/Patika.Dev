using AutoMapper;
using WebApi.DbAccess;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
	public class UpdateBookCommand
	{
		public int Id{ get; set; }
		public UpdateBookModel Model { get; set; }
		private readonly IBookStoreDbContext _context;
		private readonly IMapper _mapper;

		public UpdateBookCommand(IBookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public void Handle()
		{
			var book = _context.Books.FirstOrDefault(b => b.Id == Id);
			if (book is null)
			{
				throw new InvalidOperationException("Book not exist");
			}
			if (_context.Books.Where(b => b.Id != Id).Any(b => b.Title == Model.Title))
			{
				throw new InvalidOperationException("Book title exist");
			}

			book.Title = Model.Title != default ? Model.Title : book.Title;
			book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
			book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
			book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
			book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;

			_context.SaveChanges();
		}
	}
	public class UpdateBookModel
	{
		public string Title { get; set; }
		public int AuthorId { get; set; }
		public int GenreId { get; set; }
		public int PageCount { get; set; }
		public DateTime PublishDate { get; set; }
	}
}
