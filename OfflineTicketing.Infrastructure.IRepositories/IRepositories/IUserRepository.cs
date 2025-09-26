using OfflineTicketing.Core.Entities;

namespace OfflineTicketing.Infrastructure.IRepositories.IRepositories
{
    public interface IUserRepository
    {
        Task<Guid> CreateUser(User model);
        Task UpdateUser(User model);
        Task DeleteUser(Guid id);
        Task<List<User>> GetAllEmployees(CancellationToken ct);
        Task<List<User>> GetAllAdmins(CancellationToken ct);
        Task<User> GetById(Guid id, CancellationToken ct);
        Task<User?> GetByEmail(string email, CancellationToken ct);
    }
}
