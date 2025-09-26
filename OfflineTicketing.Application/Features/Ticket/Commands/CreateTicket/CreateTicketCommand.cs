using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Features.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<BaseResult<Guid>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTypeEnum Status { get; set; }
        public PriorityTypeEnum Priority { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid AssignedToUserId { get; set; }
    }
}
