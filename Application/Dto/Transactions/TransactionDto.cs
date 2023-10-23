﻿namespace Application.Dto.Transactions
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid? RelatedTransactionId { get; set; }
        public Guid AccountId { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get;  set; }
        public string Type { get; set; }
    }
}
