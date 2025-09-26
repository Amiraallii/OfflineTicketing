using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicket
{
    public class GetAllTicketsQuery : IRequest<BaseResult<List<TicketDto>>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
