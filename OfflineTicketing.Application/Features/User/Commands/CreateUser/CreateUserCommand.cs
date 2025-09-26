using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResult<Guid>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleTypeEnum Role { get; set; }
    }
}
