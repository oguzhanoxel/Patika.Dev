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
					new Book() {Title="Call of Cthulhu", GenreId=1, PageCount=200, PublishDate= new DateTime(2022,12,21)},
					new Book() {Title="Time Machine", GenreId=2, PageCount=220, PublishDate= new DateTime(2015,12,21)},
					new Book() {Title="Chainsawman", GenreId=3, PageCount=160, PublishDate= new DateTime(2012,12,21)},
					new Book() {Title="Attack on Titan", GenreId=3, PageCount=300, PublishDate= new DateTime(2002,12,21)},
					new Book() {Title="Herland", GenreId=2, PageCount=100, PublishDate= new DateTime(2011,12,21)}
				);

				context.SaveChanges();
			}

			if(!context.Genres.Any())
			{
				context.AddRange(
					new Genre() {Name="Horror"},
					new Genre() {Name="Science Fiction"},
					new Genre() {Name="Comic"}
				);

				context.SaveChanges();
			}
		}
	}
}
