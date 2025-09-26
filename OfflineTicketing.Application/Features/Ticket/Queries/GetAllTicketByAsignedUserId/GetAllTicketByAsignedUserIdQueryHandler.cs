using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetAllTicketByAsignedUserId
{
    public class GetAllTicketByAsignedUserIdQueryHandler(ITicketRepository repository) : IRequestHandler<GetAllTicketByAsignedUserIdQuery, BaseResult<List<TicketDto>>>
    {
        public async Task<BaseResult<List<TicketDto>>> Handle(GetAllTicketByAsignedUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await repository.GetAllTicketByAsignedUserId(request.AsignedToId, cancellationToken);
                var result = list.Select(x => new TicketDto
                {
                    AssignedToId = x.AssignedToUserId,
                    AssignedToName = x.AssignedToUser.FullName,
                    CreatedAt = x.CreatedAt,
                    CreatedById = x.CreatedByUserId,
                    CreatedByName = x.CreatedUser.FullName,
                    Description = x.Description,
                    Id = x.Id,
                    Priority = x.Priority,
                    Status = x.Status,
                    Title = x.Title,
                    UpdateAt = x.UpdatedAt
                }).ToList();

                return BaseResult<List<TicketDto>>.Success(result);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
