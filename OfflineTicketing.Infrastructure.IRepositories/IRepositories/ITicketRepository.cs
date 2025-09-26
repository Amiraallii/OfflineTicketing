using OfflineTicketing.Core.Entities;
using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Infrastructure.IRepositories.IRepositories
{
    public interface ITicketRepository
    {
        Task<Guid> CreateTicket(Ticket model);
        Task UpdateTicket(Ticket model);
        Task Delete(Guid ticketId);
        Task<Ticket?> GetTicketById(Guid id, CancellationToken ct);
        Task<List<Ticket>> GetAllTicket(int page, int count, CancellationToken ct);
        Task<List<Ticket>> GetAllTicketByAsignedUserId(Guid asignedUserId, CancellationToken ct);
        Task<List<Ticket>> GetAllTicketByCreatedUserId(Guid createdUserId, CancellationToken ct);
        Task<List<Ticket>> GetAllCurrentUserTicket(Guid userId, CancellationToken ct);
        Task UpdateStatus(Guid ticketId, StatusTypeEnum status, CancellationToken ct);
    }
}
