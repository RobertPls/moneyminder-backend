using Domain.Models.Transactions;
using SharedKernel.Core;

namespace Domain.Events.Accounts
{

    public record IncreasedAccountBalance : DomainEvent
    {
        public Guid AccountId { get; set; }
        public decimal Ammount { get; set; }

        public IncreasedAccountBalance(Guid accountId, decimal ammount) : base(DateTime.Now)
        {
            AccountId = accountId;
            Ammount = ammount;
        }
    }
}
