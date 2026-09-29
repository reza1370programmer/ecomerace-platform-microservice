
using Identity.Domain.Entities;

namespace Identity.Applications.Services;

public interface ITokenServices
{
 
    string GenerateToken(User user);
    string GenerateRefreshToken();
    int GetAccessTokenExpiresIn();
}