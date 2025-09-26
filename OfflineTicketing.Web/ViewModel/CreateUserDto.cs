using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Web.ViewModel
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleTypeEnum Role { get; set; }
    }
}
