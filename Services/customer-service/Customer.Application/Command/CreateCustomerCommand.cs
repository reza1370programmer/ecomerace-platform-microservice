using MediatR;

namespace Customer.Application.Command;

public record CreateCustomerCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber

):IRequest<Guid>;
 