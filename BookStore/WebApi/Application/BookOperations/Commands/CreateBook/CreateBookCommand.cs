using AutoMapper;
using WebApi.DbAccess;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
	public class CreateBookCommand
	{
		public CreateBookModel Model { get; set; }
		private readonly IBookStoreDbContext _context;
		private readonly IMapper _mapper;

		public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public void Handle()
		{
			var book = _context.Books.SingleOrDefault(b => b.Title == Model.Title);

			if (book is not null)
			{
				throw new InvalidOperationException("Book exist");
			}
			Book mappedBook = _mapper.Map<Book>(Model);
			_context.Books.Add(mappedBook);
			_context.SaveChanges();
		}
	}

	public class CreateBookModel
	{
		public string Title { get; set; }
		public int GenreId { get; set; }
		public int AuthorId { get; set; }
		public int PageCount { get; set; }
		public DateTime PublishDate { get; set; }
	}
}
