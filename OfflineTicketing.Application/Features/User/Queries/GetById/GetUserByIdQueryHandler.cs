using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.User.Queries.GetById
{
    public class GetUserByIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByIdQuery, BaseResult<UserDto>>
    {
        public async Task<BaseResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await repository.GetById(request.Id, cancellationToken);
                var result = new UserDto()
                {
                    Email = user.Email,
                    Role = user.Role,
                    Id = user.Id,
                    FullName = user.FullName
                };
                return BaseResult<UserDto>.Success(result);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
