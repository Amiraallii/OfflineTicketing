using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Core.Entities;
using OfflineTicketing.Infrastructure.Ef.Context;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Infrastructure.Ef.Repositories
{
    public class UserRepository(TicketingContext context) : IUserRepository
    {
        public async Task<Guid> CreateUser(User model)
        {
            await context.AddAsync(model, CancellationToken.None);
            await SaveChanges();
            return model.Id;
        }

        public async Task DeleteUser(Guid id)
        {
            if(!await DoesExist(id))
                throw new ArgumentException("User not found");
            await context.Users.Where(x=> x.Id == id).ExecuteDeleteAsync(CancellationToken.None);
        }

        public async Task<List<User>> GetAllAdmins(CancellationToken ct)
        {
            return await context.Users.Where(x => x.Role == Core.Enums.RoleTypeEnum.Admin).AsNoTracking().ToListAsync(ct);
        }

        public async Task<List<User>> GetAllEmployees(CancellationToken ct)
        {
            return await context.Users
                .Where(x => x.Role == Core.Enums.RoleTypeEnum.Employee)
                .AsNoTracking()
                .ToListAsync(ct);
            ;
        }

        public async Task<User> GetById(Guid id, CancellationToken ct)
        {
            if (!await DoesExist(id))
                throw new ArgumentException("User not found");
            return await context.Users.FindAsync([id] ,ct) ;
        }
        public async Task<User> GetByEmail(string email, CancellationToken ct) 
            => await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, ct);
        

        public async Task UpdateUser(User model)
        {
            if(!await DoesExist(model.Id))
                throw new ArgumentException("User not found"); 
            context.Users.Update(model);
            await SaveChanges();
        }
        private async Task SaveChanges() => await context.SaveChangesAsync(CancellationToken.None);
        private async Task<bool> DoesExist(Guid id) => await context.Users.AnyAsync(e => e.Id == id, CancellationToken.None);

        
    }
}
