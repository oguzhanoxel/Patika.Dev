using AutoMapper;
using WebApi.DbAccess;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommand
	{
		public int Id { get; set; }
		public UpdateAuthorModel Model { get; set; }
		private readonly BookStoreDbContext _context;

		public UpdateAuthorCommand(BookStoreDbContext context)
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
			author.Name = Model.Name != default ? Model.Name : author.Name;
			author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
			author.DateOfBirth = Model.DateOfBirth != default ? Model.DateOfBirth : author.DateOfBirth;
			_context.SaveChanges();
		}
	}

	public class UpdateAuthorModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
