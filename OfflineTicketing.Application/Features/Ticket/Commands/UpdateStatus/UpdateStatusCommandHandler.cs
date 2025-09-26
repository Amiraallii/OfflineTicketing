using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Commands.UpdateStatus
{
    public class UpdateStatusCommandHandler(ITicketRepository repository) : IRequestHandler<UpdateStatusCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
			try
			{
				await repository.UpdateStatus(request.TicketId, request.Status, cancellationToken);
				return BaseResult<bool>.Success(true);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
