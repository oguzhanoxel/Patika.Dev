using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Quaries.GetBookDetail;
using WebApi.Application.BookOperations.Quaries.GetBooks;
using WebApi.DbAccess;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase {

	private readonly IBookStoreDbContext _context;
	private readonly IMapper _mapper;

	public BooksController(IBookStoreDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IActionResult GetBooks(){
		GetBookListQuery q = new GetBookListQuery(_context, _mapper);
		var result = q.Handle();
		return Ok(result);
	}

	[HttpGet("{id}")]
	public IActionResult GetById(int id){
		GetBookDetailQuery q = new GetBookDetailQuery(_context, _mapper);
		BookViewModel result;
		q.Id = id;
		result = q.Handle();
		return Ok(result);
	}

	[HttpPost]
	public IActionResult AddBook([FromBody] CreateBookModel book){
		CreateBookCommand command = new CreateBookCommand(_context, _mapper);
		command.Model = book;
		CreateBookCommandValidator validator = new CreateBookCommandValidator();
		validator.ValidateAndThrow(command);
		command.Handle();
		return Ok();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel book){
		UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
		command.Id = id;
		command.Model = book;
		UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
		validator.ValidateAndThrow(command);
		command.Handle();
		return Ok();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteBook(int id){
		DeleteBookCommand command = new DeleteBookCommand(_context);
		command.Id = id;
		DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
		validator.ValidateAndThrow(command);
		command.Handle();
		return Ok();
	}
}
