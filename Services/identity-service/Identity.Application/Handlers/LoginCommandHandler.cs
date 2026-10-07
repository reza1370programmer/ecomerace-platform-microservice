using Domain.ValueObjects;
using Identity.Applications.Commands;
using Identity.Applications.DTO;
using Identity.Applications.Services;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Applications.Handlers;

public class LoginCommandHandler:IRequestHandler<LoginCommand,AuthResponseDto>
{
    private readonly IUserRespository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenServices _tokenServices;

    public LoginCommandHandler(IUserRespository userRepository, IRefreshTokenRepository refreshTokenRepository, IPasswordHasher passwordHasher, ITokenServices tokenServices)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _tokenServices = tokenServices;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        
        //vaildation for user data
        var email= Email.Create(request.Email);
        var user =await _userRepository.GetUSerByEmail(email, cancellationToken);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found.Invaild email or password");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("user is not active"); 
        }

        var PasswordVerify = _passwordHasher.VerifyHashedPassword(request.Password, user.PasswordHash.Value);
        if (!PasswordVerify)
        {
            throw new UnauthorizedAccessException("Email or password is incorrect"); 
        }

        
        //Genarate Tokens With Services
        var RefToken = _tokenServices.GenerateRefreshToken();
        var accessToken = _tokenServices.GenerateToken(user);
        
        
        //Refresh Repository save data in refresh tokene  table

        var Ref = RefreshToken.Create(user.Id, RefToken);
        await _refreshTokenRepository.AddRefreshToken(Ref);


        // Res some data we need
        return new AuthResponseDto()
        {
            AccessToken = accessToken,
            RefreshToken = Ref.Token,
            ExpiresIn = _tokenServices.GetAccessTokenExpiresIn() * 60,
            User = new UserDto()
            {
                UserId = user.Id.Value,
                Email = user.Email.Value,
                Role = user.Roles,
                IsActive = user.IsActive,
                CreateAt = user.CreateAt,
            }
        };

    }
}