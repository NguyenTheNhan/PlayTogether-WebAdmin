using System;

namespace WebAdmin.Shared.Models.Hirer
{

    public class HirerSummary
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
