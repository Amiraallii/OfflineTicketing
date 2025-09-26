using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Application.Dto
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTypeEnum Status { get; set; }
        public PriorityTypeEnum Priority { get; set; }
        public Guid AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public Guid CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
