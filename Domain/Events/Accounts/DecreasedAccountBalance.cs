using SharedKernel.Core;

namespace Domain.Events.Accounts
{

    public record DecreasedAccountBalance : DomainEvent
    {
        public Guid AccountId { get; set; }
        public decimal Ammount { get; set; }

        public DecreasedAccountBalance(Guid accountId, decimal ammount) : base(DateTime.Now)
        {
            AccountId = accountId;
            Ammount = ammount;
        }
    }
}
