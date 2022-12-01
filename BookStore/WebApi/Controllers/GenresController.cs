using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbAccess;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GenresController : ControllerBase
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GenresController(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetGenres()
		{
			GetGenresQuery q = new GetGenresQuery(_context, _mapper);
			var result = q.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetGenreDetail(int id)
		{
			GetGenreDetailQuery q = new GetGenreDetailQuery(_context, _mapper);
			q.Id = id;
			GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

			var result = q.Handle();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult AddGenre([FromBody] CreateGenreModel genre)
		{
			CreateGenreCommand command = new CreateGenreCommand(_context);
			command.Model = genre;
			
			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
			validator.ValidateAndThrow(command);
			
			command.Handle();
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel genre)
		{
			UpdateGenreCommand command = new UpdateGenreCommand(_context);
			command.Id = id;
			command.Model = genre;
			
			UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
			validator.ValidateAndThrow(command);
			
			command.Handle();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteGenre(int id)
		{
			DeleteGenreCommand command = new DeleteGenreCommand(_context);
			command.Id = id;
			
			DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
			validator.ValidateAndThrow(command);
			
			command.Handle();
			return Ok();
		}
	}
}
