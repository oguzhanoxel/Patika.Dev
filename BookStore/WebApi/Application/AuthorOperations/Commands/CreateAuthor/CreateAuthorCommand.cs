using AutoMapper;
using WebApi.DbAccess;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommand
	{
		public CreateAuthorModel Model { get; set; }
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public void Handle ()
		{
			if(_context.Authors.Any(a => a.Name == Model.Name && a.Surname == Model.Surname))
			{
				throw new InvalidOperationException("Author Exist.");
			}
			var mappedAuthor = _mapper.Map<Author>(Model);
			_context.Add(mappedAuthor);
			_context.SaveChanges();
		}
	}

	public class CreateAuthorModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
