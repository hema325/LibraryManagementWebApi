using Application.Books.Commands.AddBookImages;
using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.DeleteBookImages;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries;
using Application.Books.Queries.GetBookById;
using Application.Books.Queries.GetBooks;
using Application.Books.Queries.SearchBookByName;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("books")]
    public class BooksController : ApiControllerBase
    {
        private readonly ISender _sender;

        public BooksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> CreateAsync(CreateBookCommand request, CancellationToken cancellationToken)
        {
            return Created(await _sender.Send(request, cancellationToken));
        }

        [HttpPost("images")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> AddImagesAsync([FromForm] AddBookImagesCommand request, CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);
            return Created();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Update)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                throw new ValidationException(new[] { "Id in the route doesn't match id in the body" });

            await _sender.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Delete)]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteBookCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpDelete("images")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Delete)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteBookImageCommand request, CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetBookByIdQuery(id), cancellationToken));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<BookDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchBookByNameQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request,cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<BookDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetBooksAsync([FromQuery] GetBooksQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }
    }
}
