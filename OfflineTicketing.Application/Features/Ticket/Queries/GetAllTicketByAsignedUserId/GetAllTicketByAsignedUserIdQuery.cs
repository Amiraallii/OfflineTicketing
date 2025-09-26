using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicketByAsignedUserId
{
    public class GetAllTicketByAsignedUserIdQuery : IRequest<BaseResult<List<TicketDto>>>
    {
        public Guid AsignedToId { get; set; }
    }
}
