using OfflineTicketing.Core.Enums;

namespace OfflineTicketing.Web.ViewModel
{
    public class UpdateTicketStatusDto
    {
        public Guid TicketId { get; set; }
        public StatusTypeEnum Status { get; set; }
    }
}
