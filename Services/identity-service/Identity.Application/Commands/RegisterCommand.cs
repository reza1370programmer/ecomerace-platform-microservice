using MediatR;

namespace Identity.Applications.Commands;

public record RegisterCommand(string Email, string Password ,string  ConfirmPassword):IRequest<Guid>;