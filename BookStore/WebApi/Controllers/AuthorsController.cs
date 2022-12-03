using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthorList;
using WebApi.DbAccess;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase 
{
	private readonly IBookStoreDbContext _context;
	private readonly IMapper _mapper;

	public AuthorsController(IBookStoreDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IActionResult GetAuthorList()
	{
		GetAuthorListQuery q = new GetAuthorListQuery(_context, _mapper);
		var result = q.Handle();
		return Ok(result);
	}

	[HttpGet("{id}")]
	public IActionResult GetAuthorDetail(int id)
	{
		GetAuthorDetailQuery q = new GetAuthorDetailQuery(_context, _mapper);
		q.Id = id;
		GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
		validator.ValidateAndThrow(q);
		var result = q.Handle();
		return Ok(result);
	}
	
	[HttpPost]
	public IActionResult AddAuthor([FromBody] CreateAuthorModel author)
	{
		CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
		command.Model = author;

		CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
		validator.ValidateAndThrow(command);

		command.Handle();
		return Ok();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel author)
	{
		UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
		command.Id = id;
		command.Model = author;

		UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
		validator.ValidateAndThrow(command);

		command.Handle();
		return Ok();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteAuthor(int id)
	{
		DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
		command.Id = id;
		
		DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
		validator.ValidateAndThrow(command);
		
		command.Handle();
		return Ok();
	}
}
