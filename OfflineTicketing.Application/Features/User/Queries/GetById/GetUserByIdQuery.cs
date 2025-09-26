using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.User.Queries.GetById
{
    public class GetUserByIdQuery : IRequest<BaseResult<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
