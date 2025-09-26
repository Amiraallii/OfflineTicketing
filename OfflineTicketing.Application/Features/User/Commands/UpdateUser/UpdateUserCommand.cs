using MediatR;
using OfflineTicketing.Application.Dto;
using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseResult<bool>>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleTypeEnum Role { get; set; }
    }
}
