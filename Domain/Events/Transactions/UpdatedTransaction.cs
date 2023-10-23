using Domain.Models.Transactions;
using SharedKernel.Core;

namespace Domain.Events.Transactions
{
    public record UpdatedTransaction : DomainEvent
    {

        public Guid OldAccountId { get; private set; }
        public Guid NewAccountId { get; private set; }
        public decimal OldAmount { get; private set; }
        public decimal NewAmount { get; private set; }
        public TransactionType OldType { get; private set; }
        public TransactionType NewType { get; private set; }

        public UpdatedTransaction(Guid oldAccount, Guid newAccount, decimal oldAmmount, decimal newAmmount, TransactionType oldType, TransactionType newType) : base(DateTime.Now)
        {
            OldAccountId = oldAccount;
            NewAccountId = newAccount;
            OldAmount = oldAmmount;
            NewAmount = newAmmount;
            OldType = oldType;
            NewType = newType;
        }
    }
}
