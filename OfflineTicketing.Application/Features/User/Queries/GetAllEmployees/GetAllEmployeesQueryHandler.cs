using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.User.Queries.GetAllUsers
{
    public class GetAllEmployeesQueryHandler(IUserRepository repository) : IRequestHandler<GetAllEmployeesQuery, BaseResult<List<UserDto>>>
    {
        public async Task<BaseResult<List<UserDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await repository.GetAllEmployees(cancellationToken);
                var result = list.Select(x => new UserDto()
                {
                    Email = x.Email,
                    FullName = x.FullName,
                    Id = x.Id,
                    Role = x.Role
                }).ToList();
                return BaseResult<List<UserDto>>.Success(result);
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}
