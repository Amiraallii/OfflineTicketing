using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Features.Ticket.Commands.UpdateStatus
{
    public class UpdateStatusCommand : IRequest<BaseResult<bool>>
    {
        public Guid TicketId { get; set; }
        public StatusTypeEnum Status { get; set; }
    }
}
