using System;

namespace WebAdmin.Shared.Models.Hirer
{

    public class HirerSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsPlayer { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
