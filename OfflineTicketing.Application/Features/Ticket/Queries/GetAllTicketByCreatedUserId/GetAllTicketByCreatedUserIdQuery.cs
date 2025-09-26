using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicketByCreatedUserId
{
    public class GetAllTicketByCreatedUserIdQuery : IRequest<BaseResult<List<TicketDto>>>
    {
        public Guid CreatedUserId { get; set; }
    }
}
