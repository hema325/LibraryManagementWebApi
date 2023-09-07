using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.DeleteClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries;
using Application.Clients.Queries.GetClientById;
using Application.Clients.Queries.GetClients;
using Application.Clients.Queries.SearchClientByName;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("clients")]
    public class ClientsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> CreateAsync(CreateClientCommand request, CancellationToken cancellationToken)
        {
            return Created(await _sender.Send(request, cancellationToken));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Update)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateClientCommand request, CancellationToken cancellationToken)
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
            await _sender.Send(new DeleteClientCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetClientByIdQuery(id), cancellationToken));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ClientDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchClientByNameQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ClientDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetClientsAsync([FromQuery] GetClientsQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }
    }
}
