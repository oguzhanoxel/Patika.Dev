using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbAccess;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase {

	private readonly BookStoreDbContext _context;
	private readonly IMapper _mapper;

	public BooksController(BookStoreDbContext context, IMapper mapper)
	{
		BookStoreDataGenerator.Initialize();
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IActionResult GetBooks(){
		GetBooksQuery q = new GetBooksQuery(_context, _mapper);
		var result = q.Handle();
		return Ok(result);
	}

	[HttpGet("{id}")]
	public IActionResult GetById(int id){
		GetBookById q = new GetBookById(_context, _mapper);
		BookViewModel result;
		try
		{
			q.Id = id;
			result = q.Handle();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
		return Ok(result);
	}

	[HttpPost]
	public IActionResult AddBook([FromBody] CreateBookModel book){
		CreateBookCommand command = new CreateBookCommand(_context, _mapper);
		try
		{
			command.Model = book;
			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
		return Ok();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel book){
		UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
		try
		{
			command.Id = id;
			command.Model = book;
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
		return Ok();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteBook(int id){
		DeleteBookCommand command = new DeleteBookCommand(_context);
		try
		{
			command.Id = id;
			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
		return Ok();
	}
}
