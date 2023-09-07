using Application.ReservationStatuses.Commands.CreateReservationStatus;
using Application.ReservationStatuses.Commands.DeleteReservationStatus;
using Application.ReservationStatuses.Commands.UpdateReservationStatus;
using Application.ReservationStatuses.Queries;
using Application.ReservationStatuses.Queries.GetReservationStatusById;
using Application.ReservationStatuses.Queries.GetReservationStatuses;
using Application.ReservationStatuses.Queries.SearchReservationStatusByName;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("reservationStatuses")]
    public class ReservationStatusesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ReservationStatusesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> CreateAsync(CreateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            return Created(await _sender.Send(request, cancellationToken));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [MustHavePermissions(Permissions.Update)]
        public async Task<IActionResult> UpdateAsync(int id, UpdateReservationStatusCommand request, CancellationToken cancellationToken)
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
            await _sender.Send(new DeleteReservationStatusCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationStatusDto))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetReservationStatusByIdQuery(id), cancellationToken));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ReservationStatusDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchReservationStatusByNameQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ReservationStatusDto>))]
        [MustHavePermissions(Permissions.Read)]
        public async Task<IActionResult> GetReservationStatusesAsync([FromQuery] GetReservationStatusesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }
    }
}
