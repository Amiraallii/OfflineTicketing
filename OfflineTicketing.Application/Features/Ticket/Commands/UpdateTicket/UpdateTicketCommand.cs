using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Features.Ticket.Commands.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<BaseResult<bool>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTypeEnum Status { get; set; }
        public PriorityTypeEnum Priority { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
