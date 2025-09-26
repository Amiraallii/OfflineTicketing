using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfflineTicketing.Application.Features.User.Commands.CreateUser;
using OfflineTicketing.Application.IService;
using OfflineTicketing.Core.Entities;
using OfflineTicketing.Core.Enums;
using OfflineTicketing.Infrastructure.Ef.Context;
using OfflineTicketing.Infrastructure.Ef.Repositories;
using OfflineTicketing.Infrastructure.Ef.Seed;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;
using OfflineTicketing.Web.Middelware;
using OfflineTicketing.Web.WebServices;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
}); var connectionString = builder.Configuration.GetConnectionString("MainDb");

builder.Services.AddDbContext<TicketingContext>(opt =>
    opt.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddMediatR(x=> x.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
LogConfiguration.ConfigureLogging(builder.Host);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role

        };

    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", p => p.RequireRole(nameof(RoleTypeEnum.Admin)));
    options.AddPolicy("Employee", p => p.RequireRole(nameof(RoleTypeEnum.Employee)));
});
builder.Services.AddHostedService<DataSeeder>();
var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
