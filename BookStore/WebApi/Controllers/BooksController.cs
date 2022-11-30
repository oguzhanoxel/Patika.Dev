using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbAccess;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase {

	private readonly BookStoreDbContext _context;

	public BooksController(BookStoreDbContext context)
	{
		BookStoreDataGenerator.Initialize();
		_context = context;
	}

	[HttpGet]
	public IActionResult GetBooks(){
		GetBooksQuery q = new GetBooksQuery(_context);
		var result = q.Handle();
		return Ok(result);
	}

	[HttpGet("{id}")]
	public IActionResult GetById(int id){
		GetBookById q = new GetBookById(_context);
		q.Id = id;
		var result = q.Handle();
		return Ok(result);
	}

	[HttpPost]
	public IActionResult AddBook([FromBody] CreateBookModel book){
		CreateBookCommand command = new CreateBookCommand(_context);
		try
		{
			command.Model = book;
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
		UpdateBookCommand command = new UpdateBookCommand(_context);
		try
		{
			command.Id = id;
			command.Model = book;
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
		var book = _context.Books.SingleOrDefault(b => b.Id == id);
		if(book is null){
			return BadRequest();
		}

		_context.Books.Remove(book);
		_context.SaveChanges();
		return Ok();
	}
}
