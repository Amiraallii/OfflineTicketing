using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicketByCreatedUserId
{
    public class GetAllTicketByCreatedUserIdQueryHandler(ITicketRepository repository) : IRequestHandler<GetAllTicketByCreatedUserIdQuery, BaseResult<List<TicketDto>>>
    {
        public async Task<BaseResult<List<TicketDto>>> Handle(GetAllTicketByCreatedUserIdQuery request, CancellationToken cancellationToken)
        {
			try
			{
				var list = await repository.GetAllTicketByCreatedUserId(request.CreatedUserId, cancellationToken);
                var result = list.Select(x => new TicketDto
                {
                    Id = x.Id,
                    AssignedToId = x.AssignedToUserId,
                    AssignedToName = x.AssignedToUser.FullName,
                    CreatedAt = x.CreatedAt,
                    CreatedById = x.CreatedByUserId,
                    CreatedByName = x.CreatedUser.FullName,
                    Description = x.Description,
                    Priority = x.Priority,
                    Status = x.Status,
                    Title = x.Title,
                    UpdateAt = x.UpdatedAt,
                });
                return BaseResult<List<TicketDto>>.Success(result.ToList());    
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
