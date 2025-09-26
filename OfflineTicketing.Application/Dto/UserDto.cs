using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public RoleTypeEnum Role { get; set; }

    }
}
