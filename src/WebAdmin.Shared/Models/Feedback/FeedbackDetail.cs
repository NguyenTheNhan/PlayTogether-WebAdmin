﻿using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Shared.Models.Feedback
{

    public class FeedbackDetail : FeedbackSummary
    {
        public string UserId { get; set; }
        public UserDetail User { get; set; }

    }
}
