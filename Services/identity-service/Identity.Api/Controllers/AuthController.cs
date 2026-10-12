using Identity.Applications.Commands;
using Identity.Applications.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]

    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var Command = new RegisterCommand(request.Email, request.Password, request.ConfirmPassword);
            var userid = await _mediator.Send(Command);
            return Ok(userid);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
 
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] Login_Request request)
    {
        try
        {
            var coomannd = new LoginCommand(request.Email, request.Password);
            var AuthResponse = await _mediator.Send(coomannd);
            return Ok(AuthResponse);
        }
        catch (UnauthorizedAccessException ex)
        {
            return BadRequest(new
                {
                    error = ex.Message
                }
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshtokenRequest request)
    {
        try
        {
            var command = new RefreshTokenCommand(request.refreshToken);
            var AuthResponse = await _mediator.Send(command);
            return Ok(AuthResponse);
        }
        catch (UnauthorizedAccessException ex)
        { 
            return BadRequest(new
                {
                    error = ex.Message
                }
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
 
    
    
    public record RegisterRequest(string Email, string Password ,string ConfirmPassword);
    public record Login_Request(string Email, string Password);
    public record RefreshtokenRequest(string refreshToken );
}