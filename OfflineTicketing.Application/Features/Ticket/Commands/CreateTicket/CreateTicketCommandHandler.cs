using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommandHandler(ITicketRepository repository) : IRequestHandler<CreateTicketCommand, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = Core.Entities.Ticket.Create(request.Title, request.Description, request.Status, request.Priority,
                                                    request.CreatedByUserId, request.AssignedToUserId);

                var ticketId = await repository.CreateTicket(ticket);

                return BaseResult<Guid>.Success(ticketId);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
