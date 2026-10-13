using Identity.Applications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id/{id:guid}")]

    public async Task<ActionResult> GetUserById(Guid id)
    {
        var user = new GetUserByIdQuery(id);
        
        var result = await _mediator.Send(user);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);

    }

    [HttpGet("me")]
    public async Task<ActionResult> GetMe(CancellationToken cancellationToken)
    {
        var UserIdClaim=User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(UserIdClaim) || !Guid.TryParse(UserIdClaim, out Guid UserId))
        {
            return Unauthorized();
        }
        var user=await _mediator.Send(new GetUserByIdQuery(UserId));
        return Ok(user);
    }
    
}