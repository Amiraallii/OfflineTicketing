using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetTicketsByUserId
{
    public class GetTicketsByUserIdQueryHandler(ITicketRepository repository) : IRequestHandler<GetTicketsByUserIdQuery, BaseResult<List<TicketDto>>>
    {
        public async Task<BaseResult<List<TicketDto>>> Handle(GetTicketsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lis = await repository.GetAllCurrentUserTicket(request.UserId, cancellationToken);
                var result = lis.Select(x => new TicketDto
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
