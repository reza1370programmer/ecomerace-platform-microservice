using Identity.Applications.DTO;
using MediatR;

namespace Identity.Applications.Commands;

public record RefreshTokenCommand(string RefreshToken):IRequest<AuthResponseDto>;