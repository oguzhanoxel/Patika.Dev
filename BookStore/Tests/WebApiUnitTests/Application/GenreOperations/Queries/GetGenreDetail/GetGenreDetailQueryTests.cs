using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbAccess;
using WebApi.Entities;
using WebApiUnitTests.TestSetup;

namespace WebApiUnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDbContext _context;
		private	readonly IMapper _mapper;

		public GetGenreDetailQueryTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]
		public void WhenNonExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			GetGenreDetailQuery q = new GetGenreDetailQuery(_context, null);

			FluentActions
			.Invoking(() => q.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found");
		}

		[Fact]
		public void WhenValidInputAreGiven_GenreViewModel_ShouldBeReturn()
		{
			var genre = new Genre() { Name="Get Genre" };
			_context.Genres.Add(genre);
			_context.SaveChanges();

			GetGenreDetailQuery q = new GetGenreDetailQuery(_context, _mapper);
			q.Id = genre.Id;
			var mappedGenre = _mapper.Map<GenreDetailViewModel>(_context.Genres.Find(genre.Id));

			FluentActions
			.Invoking(() => q.Handle()).Invoke();
			var returnGenre = q.Handle();

			returnGenre.Name.Should().Be(mappedGenre.Name);
		}
	}
}
