using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.API.Common;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ApiConventionType(typeof(DefaultApiConventions))]
public class ApiControllerBase : ControllerBase
{
    protected readonly IMediator Mediator;
    public ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}