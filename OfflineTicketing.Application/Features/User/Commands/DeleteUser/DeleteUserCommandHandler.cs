using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
			try
			{
				await repository.DeleteUser(request.Id);
                return BaseResult<bool>.Success(true);
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
