using Domain.ValueObjects;
using Identity.Applications.Commands;
using Identity.Applications.Services;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;
using MediatR;


namespace Identity.Applications.Handlers;

public class RegisterCommandHandler:IRequestHandler<RegisterCommand,Guid>
{
    private readonly IUserRespository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRespository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        if (await _userRepository.UserEmailExists(email,cancellationToken))
        {
            throw new InvalidOperationException($"User  With  Email {request.Email} already exists");
            
        }

        var hashpassword = _passwordHasher.HashPassword(request.Password);
        var UserPasswordHash = PasswordHash.Create(hashpassword);
        var user = User.Create(email, UserPasswordHash);
        await _userRepository.AddUser(user, cancellationToken);
        return user.Id.Value;
    }
}