using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfflineTicketing.Application.IService;
using OfflineTicketing.Core.Entities;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;
using OfflineTicketing.Web.ViewModel;

namespace OfflineTicketing.Web.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController(IUserRepository repo,
        IPasswordHasher<User> hasher,
        IJwtTokenGenerator jwt) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            var user = await repo.GetByEmail(dto.Email, ct);
            if (user is null) return Unauthorized("Invalid credentials");

            var vr = hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (vr == PasswordVerificationResult.Failed) return Unauthorized("Invalid credentials");

            var token = jwt.GenerateToken(user);
            return Ok(new { token, user = new { user.Id, user.FullName, user.Email, user.Role } });
        }
    }
}
