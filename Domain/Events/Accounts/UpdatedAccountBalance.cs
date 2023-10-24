using Domain.Models.Transactions;
using SharedKernel.Core;

namespace Domain.Events.Accounts
{

    public record UpdatedAccountBalance : DomainEvent
    {
        public Guid AccountId { get; set; }
        public decimal OldAmmount { get; set; }
        public decimal NewAmmount { get; set; }



        public UpdatedAccountBalance(Guid accountId, decimal oldAmmount, decimal newAmmount) : base(DateTime.Now)
        {
            AccountId = accountId;
            OldAmmount = oldAmmount;
            NewAmmount = newAmmount;
        }
    }
}
