using System;

namespace WebAdmin.Shared.Models.Transaction
{

    public class TransactionSummary
    {
        public string Id { get; set; }
        public string Operation { get; set; }
        public float Money { get; set; }
        public string TypeOfTransaction { get; set; }
        public string ReferenceTransactionId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
