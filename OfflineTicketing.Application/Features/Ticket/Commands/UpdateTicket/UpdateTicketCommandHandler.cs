using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler(ITicketRepository repository) : IRequestHandler<UpdateTicketCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
			try
			{
				var ticket = await repository.GetTicketById(request.Id, cancellationToken);

                if (ticket == null)
                    return BaseResult<bool>.Failure("Ticket not found");

                ticket.UpdateTitle(request.Title);
                ticket.UpdateDescription(request.Description);
                ticket.UpdatePriority(request.Priority);
                ticket.UpdateStatus(request.Status);
                ticket.SetUpdateDate(DateTime.UtcNow);
                ticket.UpdateAssignedUSer(request.AssignedTo);
                await repository.UpdateTicket(ticket);

                return BaseResult<bool>.Success(true);
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
