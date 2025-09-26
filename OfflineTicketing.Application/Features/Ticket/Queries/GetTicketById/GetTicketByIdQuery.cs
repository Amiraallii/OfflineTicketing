using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<BaseResult<TicketDto>>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
