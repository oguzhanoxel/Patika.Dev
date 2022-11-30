using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.Common;

namespace WebApi.Mapping
{
	public class MappingProfile:Profile{
		public MappingProfile()
		{
			CreateMap<CreateBookModel, Book>().ReverseMap();
			CreateMap<UpdateBookModel, Book>().ReverseMap();
			CreateMap<BooksViewModel, Book>().ReverseMap().ForMember(x => x.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
			CreateMap<BookViewModel, Book>().ReverseMap().ForMember(x => x.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
		}
	}
}
