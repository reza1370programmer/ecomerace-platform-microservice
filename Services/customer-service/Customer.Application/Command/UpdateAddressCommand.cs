using MediatR;

namespace Customer.Application.Command;

public record UpdateAddressCommand(
    Guid CustomerId,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country
) : IRequest;

    
 