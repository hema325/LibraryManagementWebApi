using Application.ClientStatuses.Commands.CreateClientStatus;
using Application.ClientStatuses.Commands.DeleteClientStatus;
using Application.ClientStatuses.Commands.UpdateClientStatus;
using Application.ClientStatuses.Queries;
using Application.ClientStatuses.Queries.GetClientStatusById;
using Application.ClientStatuses.Queries.GetClientStatuses;
using Application.ClientStatuses.Queries.SearchClientStatusByName;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("clientStatuses")]
    public class ClientStatusesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ClientStatusesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> CreateAsync(CreateClientStatusCommand request, CancellationToken cancellationToken)
        {
            return Created(await _sender.Send(request, cancellationToken));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Update)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateClientStatusCommand request, CancellationToken cancellationToken)
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
            await _sender.Send(new DeleteClientStatusCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientStatus))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetClientStatusByIdQuery(id), cancellationToken));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ClientStatusDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchClientStatusByNameQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ClientStatusDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetClientStatusesAsync([FromQuery] GetClientStatusesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }
    }
}
