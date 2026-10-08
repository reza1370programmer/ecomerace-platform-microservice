using Identity.Applications.DTO;
using Identity.Applications.Queries;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;
using MediatR;

namespace Identity.Applications.Handlers;

public class GetUserByIdQueryHandler:IRequestHandler<GetUserByIdQuery,UserDto>
{
    
     private readonly IUserRespository _userRepository;

     public GetUserByIdQueryHandler(IUserRespository userRepository)
     {
         _userRepository = userRepository;
     }


     public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userid=UserId.Create(request.UserId);
        var user = await _userRepository.GetUSerById(userid);
        if (user == null)
        {
            return null;
        }

        return new UserDto()
        {
            Email = user.Email.Value,
            Role = user.Roles,
            IsActive = user.IsActive,
            CreateAt = user.CreateAt,
            UserId = user.Id.Value
        };
    }
}