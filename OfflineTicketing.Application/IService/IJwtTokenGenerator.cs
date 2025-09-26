using OfflineTicketing.Core.Entities;

namespace OfflineTicketing.Application.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);

    }
}
