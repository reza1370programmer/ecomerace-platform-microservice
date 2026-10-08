using Identity.Applications.Commands;
using Identity.Applications.DTO;
using Identity.Applications.Services;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Applications.Handlers;

public class RefreshTokenCommandHandler :IRequestHandler<RefreshTokenCommand,AuthResponseDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRespository _userRepository;
    private readonly ITokenServices _tokenServices;


    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRespository userRepository, ITokenServices tokenServices)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _tokenServices = tokenServices;
    }


    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refresh= await _refreshTokenRepository.GetRefreshToken(request.RefreshToken ,cancellationToken);
        if (refresh == null ||!refresh.IsVaild())
        {
            throw new UnauthorizedAccessException("invaild refresh token");
        }

        var _User = await _userRepository.GetUSerById(refresh.UserId, cancellationToken);
        if (_User == null || !_User.IsActive)
        {
            throw new UnauthorizedAccessException("invaild user");
        }
        
        refresh.Revoke();//we consume that
        await _refreshTokenRepository.UpdateRefreshToken(refresh, cancellationToken);

        var accessToken = _tokenServices.GenerateToken(_User);
        var NewRefreshToken = _tokenServices.GenerateRefreshToken();
        var RefreshTokenentity = RefreshToken.Create(_User.Id, NewRefreshToken);
        await _refreshTokenRepository.AddRefreshToken(RefreshTokenentity, cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = RefreshTokenentity.Token,
            ExpiresIn = _tokenServices.GetAccessTokenExpiresIn() * 60,
            User = new UserDto()
            {
                IsActive = _User.IsActive,
                CreateAt = _User.CreateAt,
                Email = _User.Email.Value,
                Role = _User.Roles,
                UserId = _User.Id.Value
            }
        };

    }
}