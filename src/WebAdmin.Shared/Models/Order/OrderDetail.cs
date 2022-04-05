using System;
using System.Collections.Generic;
using WebAdmin.Shared.Models.Game;
using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Shared.Models.Order
{

    public class OrderDetail
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserDetail User { get; set; }
        public string ToUserId { get; set; }
        public UserDetail ToUser { get; set; }
        public GameOfOrder[] GameOfOrders { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public int TotalTimes { get; set; }
        public float TotalPrices { get; set; }
        public float FinalPrices { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public IEnumerable<ReportInOrder> Reports { get; set; }
        public string Status { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public DateTime ProcessExpired { get; set; }
    }



    public class GameOfOrder
    {
        public string Id { get; set; }
        public string GameId { get; set; }
        public GameSummary Game { get; set; }
    }

    public class Rating
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public float Rate { get; set; }
        public bool IsViolate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
