using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Core.Entities
{
    public class Ticket 
    {
        #region ' Ctor '
        private Ticket() {}

        private Ticket(Guid id, string title, string description, StatusTypeEnum status,
            PriorityTypeEnum priority, DateTime createdAt, DateTime? updatedAt,
            Guid createdByUserId, Guid assignedToUserId)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            CreatedByUserId = createdByUserId;
            AssignedToUserId = assignedToUserId;
        }

        #endregion ' Ctor '

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public StatusTypeEnum Status { get; private set; }
        public PriorityTypeEnum Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public Guid AssignedToUserId { get; private set; }

        #region ' Relation '
        public User CreatedUser { get; private set; }
        public User AssignedToUser { get; private set; }
        #endregion ' Relation '


        #region ' Actions '
        public static Ticket Create(string title, string description, StatusTypeEnum status,
            PriorityTypeEnum priority, Guid createdByUserId, Guid assignedToUserId)
        {
            var ticket = new Ticket(Guid.NewGuid(), title, description, status, priority,
                                    DateTime.UtcNow, null, createdByUserId, assignedToUserId);
            return ticket;
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateAssignedUSer(Guid assignedUserId)
        {
            AssignedToUserId = assignedUserId;
        }
        public void UpdateDescription(string description)
        {
            Description = description;
        }
        public void UpdateStatus(StatusTypeEnum status)
        {
            Status = status;
        }
        public void UpdatePriority(PriorityTypeEnum priority)
        {
            Priority = priority;
        }
        public void SetUpdateDate(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
        #endregion ' Actions '
    }
}
