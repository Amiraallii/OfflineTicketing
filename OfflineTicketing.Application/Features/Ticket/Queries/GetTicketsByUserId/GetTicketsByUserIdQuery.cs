using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetTicketsByUserId
{
    public class GetTicketsByUserIdQuery : IRequest<BaseResult<List<TicketDto>>>
    {
        public Guid UserId { get; set; }
    }
}
