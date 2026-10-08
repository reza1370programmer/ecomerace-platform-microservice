using Identity.Applications.DTO;
using MediatR;

namespace Identity.Applications.Queries;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;