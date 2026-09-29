
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Applications.Services;
using Identity.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Identity.Infrastucture.Services;

public class TokenServices:ITokenServices
{
    private readonly IConfiguration _configuration;

    public TokenServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
       var secret =_configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT secret not found");
       var issure =  _configuration["Jwt:Issure"] ?? "ECommercePlatform";
       var audience = _configuration["Jwt:Audience"] ?? "ECommercePlatform";
       var expirationsMinutes = int.Parse(_configuration["Jwt:ExpirationsMinutes"]?? "60") ;
       var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
       var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
       var claims=new List<Claim>()
       {
           new (ClaimTypes.NameIdentifier,user.Id.Value.ToString()),
           new (ClaimTypes.Email,user.Email.Value.ToString()),
           new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
       };

       foreach (var role in user.Roles)
       {
           claims.Add(new Claim(ClaimTypes.Role,role));
       }

       var Token = new JwtSecurityToken(
           issuer: issure,
           audience: audience,
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(expirationsMinutes),
           signingCredentials: signingCredentials
       );
       
       return new JwtSecurityTokenHandler().WriteToken(Token);
    }

    public string GenerateRefreshToken()
    {
       var randomNumber = new byte[64];
       using var rng = RandomNumberGenerator.Create();
       rng.GetBytes(randomNumber);
       return Convert.ToBase64String(randomNumber);
    }

    public int GetAccessTokenExpiresIn()
    {
        return  int.Parse(_configuration["Jwt:ExpirationsMinutes"]?? "60") ;
    }
}