using System;

namespace WebAdmin.Shared.Models.Feedback
{

    public class FeedbackSummary
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string TypeOfFeedback { get; set; }
        public bool? IsApprove { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
