using Microsoft.AspNetCore.Mvc;
using WebApi.DbAccess;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase {

	private readonly BookStoreDbContext _context;

	public BooksController(BookStoreDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public List<Book> GetBooks(){
		var bookList = _context.Books.OrderBy(b => b.Id).ToList();
		return bookList;
	}

	[HttpGet("{id}")]
	public Book GetById(int id){
		var book = _context.Books.Where(b => b.Id == id).SingleOrDefault();
		return book;
	}

	[HttpPost]
	public IActionResult AddBook([FromBody] Book newBook){
		var book = _context.Books.SingleOrDefault(b => b.Title == newBook.Title);

		if(book is not null){
			return BadRequest();
		}
		_context.Add(newBook);
		_context.SaveChanges();
		return Ok();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
		var book = _context.Books.SingleOrDefault(b => b.Id == id);
		if(book is null){
			return BadRequest();
		}
		_context.Update(updatedBook);
		_context.SaveChanges();

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
