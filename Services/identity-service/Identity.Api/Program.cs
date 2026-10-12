using Identity.Applications.Commands;
using Identity.Applications.Services;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence.Efcore;
using Identity.Infrastructure.Persistence.Repositories;
using Identity.Infrastucture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<IdentityContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//madiatR
builder.Services.AddMediatR(cnf => cnf.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
//Di
builder.Services.AddScoped<IUserRespository, UserRespository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var JwtSecret = builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("jwt secret mot configured!");
var JwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "ECommercePlatform";
var JwtAudience = builder.Configuration["Jwt:Audience"] ?? "ECommercePlatform";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtIssuer,
        ValidAudience = JwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret))
    };
});
builder.Services.AddAuthenticationCore();

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(op =>
    {
        op.AllowAnyHeader();
        op.AllowAnyMethod();
        op.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
