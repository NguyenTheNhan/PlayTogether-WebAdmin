using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Models.Order;

namespace WebAdmin.Shared.Models.Report
{

    public class ReportDetails : ReportSummary
    {
        public string UserId { get; set; }
        public UserDetail User { get; set; }
        public string ToUserId { get; set; }
        public UserDetail ToUser { get; set; }
        public OrderDetail Order { get; set; }
    }

}
