using System;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Shared.Models.Rating
{

    public class RatingDetail
    {
        public string Id { get; set; }
        public HirerSummary User { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public bool? IsViolate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsApprove { get; set; }
    }


}
