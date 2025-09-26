using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Web.ViewModel
{
    public class UpdateTicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTypeEnum Status { get; set; }
        public PriorityTypeEnum Priority { get; set; }
        public Guid AssignedToUserId { get; set; }
    }
}
