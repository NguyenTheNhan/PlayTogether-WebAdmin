using System;

namespace WebAdmin.Shared.Models.Hirer
{

    public class UserDetail : HirerSummary
    {

        public string Avatar { get; set; }
        public bool IsPlayer { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public bool Gender { get; set; }
        public Image[] Images { get; set; }
        public string Description { get; set; }
        public float Rate { get; set; }
        public float pricePerHour { get; set; }
        public int maxHourHire { get; set; }
    }

    public class Image
    {
        public string Id { get; set; }
        public string ImageLink { get; set; }
    }

}
