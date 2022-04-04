using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Models.Order;

namespace WebAdmin.Shared.Models.Report
{

    public class ReportDetail
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public HirerSummary User { get; set; }
        public string ToUserId { get; set; }
        public HirerSummary ToUser { get; set; }
        public string ReportMessage { get; set; }
        public OrderDetail Order { get; set; }
    }

}
