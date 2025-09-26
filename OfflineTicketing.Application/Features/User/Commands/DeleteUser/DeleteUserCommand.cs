using MediatR;
using OfflineTicketing.Application.Dto;

namespace OfflineTicketing.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
