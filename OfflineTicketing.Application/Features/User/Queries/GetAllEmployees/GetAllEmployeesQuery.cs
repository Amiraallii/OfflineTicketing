using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.User.Queries.GetAllUsers
{
    public class GetAllEmployeesQuery : IRequest<BaseResult<List<UserDto>>>
    {
    }
}
