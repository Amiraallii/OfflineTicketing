using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler(ITicketRepository repository) : IRequestHandler<DeleteTicketCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
			try
			{
				await repository.Delete(request.Id);
                return BaseResult<bool>.Success(true);
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
