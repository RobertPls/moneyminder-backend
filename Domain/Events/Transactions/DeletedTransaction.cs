using Domain.Models.Transactions;
using SharedKernel.Core;

namespace Domain.Events.Transactions
{
    public record DeletedTransaction : DomainEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }


        public DeletedTransaction(Guid accountId, decimal amount, TransactionType type) : base(DateTime.Now)
        {
            AccountId = accountId;
            Amount = amount;
            Type = type;
        }
    }
}
