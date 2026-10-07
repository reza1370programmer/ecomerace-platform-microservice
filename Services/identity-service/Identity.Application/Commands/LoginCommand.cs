using Identity.Applications.DTO;
using MediatR;

namespace Identity.Applications.Commands;

public record LoginCommand(string Email, string Password):IRequest<AuthResponseDto>;