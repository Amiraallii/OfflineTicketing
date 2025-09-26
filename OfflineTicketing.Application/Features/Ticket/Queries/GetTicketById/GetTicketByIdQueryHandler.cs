using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.Ticket.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler(ITicketRepository repository) : IRequestHandler<GetTicketByIdQuery, BaseResult<TicketDto>>
    {
        public async Task<BaseResult<TicketDto>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
			try
			{
				var model = await repository.GetTicketById(request.Id, cancellationToken);
                
                    return BaseResult<TicketDto>.Success(new TicketDto
                    {
                        Id = model.Id,
                        AssignedToId = model.AssignedToUserId,
                        AssignedToName = model.AssignedToUser.FullName,
                        CreatedById = model.CreatedByUserId,
                        CreatedByName = model.CreatedUser.FullName,
                        Description = model.Description,
                        Priority = model.Priority,
                        Status = model.Status,
                        Title = model.Title,
                        UpdateAt = model.UpdatedAt,
                        CreatedAt = model.CreatedAt,


                    });
                
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
