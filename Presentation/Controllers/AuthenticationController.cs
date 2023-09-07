using Application.Authentication.Commands.Authenticate;
using Application.Authentication.Commands.ChangePermissions;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.RequestJwtToken;
using Application.Authentication.Commands.RevokeRefreshToken;
using Application.Authentication.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("authentication")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [HaveRoles(Roles.Manager)]
        [MustHavePermissions(Permissions.Add)]
        public async Task<IActionResult> RegisterAsync(RegisterCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }

        [HttpPut("changeUserPermissions/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Manager)]
        [MustHavePermissions(Permissions.Update)]
        public async Task<IActionResult> ChangePermissionsAsync(int id,ChangePermissionsCommand request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                throw new ValidationException(new[] { "Id in the route doesn't match id in the body" });

            await _sender.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpPost("requestJwtToken")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [AllowAnonymous]
        public async Task<IActionResult> RequestJwtTokenAsync(RequestJwtTokenCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(request, cancellationToken));
        }

        [HttpPost("revokeRefreshToken")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ExceptionResult))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeRefreshTokenAsync(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);
            return NoContent();
        }
    }
}
