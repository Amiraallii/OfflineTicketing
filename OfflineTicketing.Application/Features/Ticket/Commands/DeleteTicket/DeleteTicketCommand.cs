using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.Ticket.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<BaseResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
