using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Core.Entities;
using OfflineTicketing.Core.Enums;
using OfflineTicketing.Infrastructure.Ef.Context;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Infrastructure.Ef.Repositories
{
    public class TicketRepository(TicketingContext context) : ITicketRepository
    {
        public async Task<Guid> CreateTicket(Ticket model)
        {
            await context.AddAsync(model, CancellationToken.None);
            await SaveChanges();
            return model.Id;
        }

        public async Task Delete(Guid ticketId)
        {
            if (!await DoesExist(ticketId))
                throw new ArgumentException("Ticket not found");
            await context.Tickets.Where(x => x.Id == ticketId).ExecuteDeleteAsync(CancellationToken.None);
        }

        public async Task<List<Ticket>> GetAllTicket(int page, int count, CancellationToken ct)
        {
            return await context.Tickets
                .OrderByDescending(t => t.Priority)
                .ThenByDescending(x => x.CreatedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .Include(t => t.CreatedUser)
                .Include(t => t.AssignedToUser)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<List<Ticket>> GetAllTicketByAsignedUserId(Guid asignedUserId, CancellationToken ct)
        {
            return await context.Tickets
                .Include(t => t.CreatedUser)
                .Include(t => t.AssignedToUser)
                .AsNoTracking()
                .Where(context => context.AssignedToUserId == asignedUserId)
                .OrderByDescending(t => t.Priority)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync(ct);
        }

        public async Task<List<Ticket>> GetAllTicketByCreatedUserId(Guid createdUserId, CancellationToken ct)
        {
            return await context.Tickets
                .Include(t => t.CreatedUser)
                .Include(t => t.AssignedToUser)
                .AsNoTracking()
                .Where(context => context.CreatedByUserId == createdUserId)
                .OrderByDescending(t => t.Priority)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync(ct);
        }

        public async Task<Ticket> GetTicketById(Guid id, CancellationToken ct)
        {
            if (!await DoesExist(id))
                throw new ArgumentException("Ticket not found");

            return await context.Tickets.Include(x => x.AssignedToUser).Include(x => x.CreatedUser).FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Ticket>> GetAllCurrentUserTicket(Guid userId, CancellationToken ct)
        {
            return await context.Tickets
                .Where(x => x.CreatedByUserId == userId)
                .Include(x => x.AssignedToUser)
                .Include(x => x.CreatedUser)
                .ToListAsync(ct);
        }

        public async Task UpdateTicket(Ticket model)
        {
            if (!await DoesExist(model.Id))
                throw new ArgumentException("Ticket not found");
            if (await IsProgressed(model.Id))
                throw new ArgumentException("You can not edit ticket because is not open any more");
            context.Tickets.Update(model);
            await SaveChanges();
        }

        public async Task UpdateStatus(Guid ticketId, StatusTypeEnum status, CancellationToken ct)
        {
            if (!await DoesExist(ticketId))
                throw new ArgumentException("Ticket not found");
            await context.Tickets
                .Where(x => x.Id == ticketId)
                .ExecuteUpdateAsync(x => x.SetProperty(f => f.Status, status), ct);
        }

        private async Task SaveChanges() => await context.SaveChangesAsync(CancellationToken.None);
        private async Task<bool> DoesExist(Guid id) => await context.Tickets.AnyAsync(x => x.Id == id, CancellationToken.None);
        private async Task<bool> IsProgressed(Guid id) => await context.Tickets.AnyAsync(x => x.Id == id && x.Status > StatusTypeEnum.Open, CancellationToken.None);
    }
}
