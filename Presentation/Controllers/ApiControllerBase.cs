using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [HaveRoles(Roles.Manager,Roles.Employee)]
    public class ApiControllerBase : ControllerBase 
    {
        protected IActionResult Created<TData>(TData data)
            => StatusCode(StatusCodes.Status201Created, data);

        protected IActionResult Created()
            => StatusCode(StatusCodes.Status201Created);
    }
}
