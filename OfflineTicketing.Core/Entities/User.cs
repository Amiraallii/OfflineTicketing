using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Core.Entities
{
    public class User 
    {
        #region ' Ctor '
        private User() {}

        private User(Guid id, string fullName, string email, string password, RoleTypeEnum role)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Password = password;
            Role = role;
        }

        #endregion ' Ctor '
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public RoleTypeEnum Role { get; private set; }

        #region ' Relation '
        public ICollection<Ticket> CreatedTickets { get; private set; }
        public ICollection<Ticket> AssignedTickets { get; private set; }
        #endregion ' Relation '


        #region ' Actions '
        public static User Create(string fullName, string email, string password, RoleTypeEnum role)
        {
            var user = new User(Guid.NewGuid(), fullName, email, password, role);
            return user;
        }
        public void UpdateRole(RoleTypeEnum role)
        {
            Role = role;
        }
        public void UpdateEmail(string email)
        {
            Email = email;
        }
        public void UpdateFullName(string fullName)
        {
            FullName = fullName;
        }
        public void UpdatePassword(string password)
        {
            Password = password;
        }
        #endregion ' Actions '
    }
}
