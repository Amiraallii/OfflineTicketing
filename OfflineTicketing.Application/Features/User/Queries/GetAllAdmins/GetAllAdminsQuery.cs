using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.User.Queries.GetAllAdmins
{
    public class GetAllAdminsQuery : IRequest<BaseResult<List<UserDto>>>
    {
    }
}
