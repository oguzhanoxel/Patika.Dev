using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthorList;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Quaries.GetBookDetail;
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
			
			CreateMap<BooksViewModel, Book>().ReverseMap()
			.ForMember(x => x.Genre, opt => opt.MapFrom(src => src.Genre.Name))
			.ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author.Name));
			
			CreateMap<BookViewModel, Book>().ReverseMap()
			.ForMember(x => x.Genre, opt => opt.MapFrom(src => src.Genre.Name))
			.ForMember(x => x.Author, opt => opt.MapFrom(src => src.Author.Name));

			// Genre
			CreateMap<GenreViewModel, Genre>().ReverseMap();
			CreateMap<GenreDetailViewModel, Genre>().ReverseMap();

			// Author
			CreateMap<CreateAuthorModel, Author>().ReverseMap();
			CreateMap<UpdateAuthorModel, Author>().ReverseMap();
			CreateMap<AuthorModel, Author>().ReverseMap();
			CreateMap<AuthorDetailModel, Author>().ReverseMap();
		}
	}
}
