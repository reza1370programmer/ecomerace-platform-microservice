

using Identity.Api.Protos;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;

namespace Identity.Api.services;

public class UserServicer : UserProtoServic.UserProtoServicBase
{
    private readonly IUserRespository userRepository;
    private readonly ILogger<UserServicer> logger;
    public UserServicer(IUserRespository userRepository, ILogger<UserServicer> logger)
    {
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<CheckUserExistsResponse> CheckUserExists(CheckUserExistsRequest request)
    {
        logger.LogInformation("grpc request received");

        var userid = new UserId(Guid.Parse(request.UserId));
        var user = await userRepository.GetUSerById(userid);
        return new CheckUserExistsResponse()
        {
            Exists = user != null
        };
    }
}