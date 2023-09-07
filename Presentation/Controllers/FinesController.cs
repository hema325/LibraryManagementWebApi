using Application.Fines.Commands.CreateFine;
using Application.Fines.Commands.DeleteFine;
using Application.Fines.Commands.UpdateFine;
using Application.Fines.Queries;
using Application.Fines.Queries.GetFineById;
using Application.Fines.Queries.GetFines;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("fines")]
    public class FinesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public FinesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> CreateAsync(CreateFineCommand request, CancellationToken cancellationToken)
        {
            return Created(await _sender.Send(request, cancellationToken));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Update)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateFineCommand request, CancellationToken cancellationToken)
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
            await _sender.Send(new DeleteFineCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FineDto))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetFineByIdQuery(id), cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<FineDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetFinesAsync([FromQuery] GetFinesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }
    }
}
