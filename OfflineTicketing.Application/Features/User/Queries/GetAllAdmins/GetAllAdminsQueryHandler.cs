using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.User.Queries.GetAllAdmins
{
    public class GetAllAdminsQueryHandler(IUserRepository repository) : IRequestHandler<GetAllAdminsQuery, BaseResult<List<UserDto>>>
    {
        public async Task<BaseResult<List<UserDto>>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await repository.GetAllAdmins(cancellationToken);
                var result = list.Select(x => new UserDto()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
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
