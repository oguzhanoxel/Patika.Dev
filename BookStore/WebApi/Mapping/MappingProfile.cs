using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Quaries.GetBookById;
using WebApi.Application.BookOperations.Quaries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Mapping
{
	public class MappingProfile:Profile{
		public MappingProfile()
		{
			// Book
			CreateMap<CreateBookModel, Book>().ReverseMap();
			CreateMap<UpdateBookModel, Book>().ReverseMap();
			CreateMap<BooksViewModel, Book>().ReverseMap().ForMember(x => x.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<BookViewModel, Book>().ReverseMap().ForMember(x => x.Genre, opt => opt.MapFrom(src => src.Genre.Name));

			// Genre
			CreateMap<GenreViewModel, Genre>().ReverseMap();
			CreateMap<GenreDetailViewModel, Genre>().ReverseMap();
		}
	}
}
