using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase {
	private static List<Book> BookList = new List<Book>() {
		new Book {Id=1, Title="Time Machine", GenreId=1, PageCount=200, PublishDate= new DateTime(2021,10,11)},
		new Book {Id=2, Title="Herland", GenreId=1, PageCount=220, PublishDate= new DateTime(2011,10,22)},
		new Book {Id=3, Title="Call of Cthulhu", GenreId=3, PageCount=110, PublishDate= new DateTime(2001,10,11)},
	};

	[HttpGet]
	public List<Book> GetBooks(){
		var bookList = BookList.OrderBy(b => b.Id).ToList();
		return bookList;
	}

	[HttpGet("{id}")]
	public Book GetById(int id){
		var book = BookList.Where(b => b.Id == id).SingleOrDefault();
		return book;
	}

	[HttpPost]
	public IActionResult AddBook([FromBody] Book newBook){
		var book = BookList.SingleOrDefault(b => b.Title == newBook.Title);

		if(book is not null){
			return BadRequest();
		}
		BookList.Add(newBook);
		return Ok();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
		var book = BookList.SingleOrDefault(b => b.Id == id);
		if(book is null){
			return BadRequest();
		}
		book.GenreId = updatedBook != default ? updatedBook.GenreId : book.GenreId;
		book.PageCount = updatedBook != default ? updatedBook.PageCount : book.PageCount;
		book.Title = updatedBook != default ? updatedBook.Title : book.Title;
		book.PublishDate = updatedBook != default ? updatedBook.PublishDate : book.PublishDate;

		return Ok();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteBook(int id){
		var book = BookList.SingleOrDefault(b => b.Id == id);
		if(book is null){
			return BadRequest();
		}

		BookList.Remove(book);
		return Ok();
	}
}
