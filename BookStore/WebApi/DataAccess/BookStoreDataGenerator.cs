using WebApi.DbAccess;
using WebApi.Entities;

public class BookStoreDataGenerator
{
	public static void Initialize(){
		using (var context = new BookStoreDbContext())
		{
			if(!context.Books.Any())
			{
				context.AddRange(
					new Book() {Title="Call of Cthulhu", AuthorId=3, GenreId=1, PageCount=200, PublishDate= new DateTime(2022,12,21)},
					new Book() {Title="Time Machine", AuthorId=2, GenreId=2, PageCount=220, PublishDate= new DateTime(2015,12,21)},
					new Book() {Title="Attack on Titan", AuthorId=4, GenreId=3, PageCount=300, PublishDate= new DateTime(2002,12,21)},
					new Book() {Title="Herland", AuthorId=1, GenreId=1, PageCount=100, PublishDate= new DateTime(2011,12,21)}
				);

				context.SaveChanges();
			}

			if(!context.Genres.Any())
			{
				context.AddRange(
					new Genre() {Name="Horror"},
					new Genre() {Name="Science Fiction"},
					new Genre() {Name="Post-Apocalyptic"}
				);

				context.SaveChanges();
			}

			if(!context.Authors.Any())
			{
				context.AddRange(
					new Author() {Name="Charlotte Perkins", Surname="Gilman"},
					new Author() {Name="Herbert George", Surname="Wells"},
					new Author() {Name="Howard Phillips", Surname="Lovecraft"},
					new Author() {Name="Hajime", Surname="Isayama"}
				);

				context.SaveChanges();
			}
		}
	}
}
