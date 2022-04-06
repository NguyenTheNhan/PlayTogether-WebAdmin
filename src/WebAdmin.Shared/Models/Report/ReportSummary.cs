using System;

namespace WebAdmin.Shared.Models.Report
{

    public class ReportSummary
    {
        public string Id { get; set; }
        public string ReportMessage { get; set; }
        public bool? IsApprove { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
