using MediatR;
using Microsoft.AspNetCore.Identity;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository repository, IPasswordHasher<Core.Entities.User> _hasher) : IRequestHandler<CreateUserCommand, BaseResult<Guid>>
    {
        public async Task<BaseResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = Core.Entities.User.Create(request.FullName, request.Email, request.Password, request.Role);
                var hashed = _hasher.HashPassword(user, request.Password);
                user.UpdatePassword(hashed);
                await repository.CreateUser(user);
                return BaseResult<Guid>.Success(user.Id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
