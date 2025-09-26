using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = await repository.GetById(request.Id, cancellationToken);
                if (model == null)
                    return BaseResult<bool>.Failure("User not found");
                model.UpdateRole(request.Role);
                model.UpdateEmail(request.Email);
                model.UpdateFullName(request.FullName);
                model.UpdatePassword(request.Password);
                await repository.UpdateUser(model);
                return BaseResult<bool>.Success(true);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
