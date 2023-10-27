using Domain.Models.Transactions;
using SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.Accounts
{

    public record UpdatedAccountBalance : DomainEvent
    {
        public Guid AccountId { get; set; }
        public decimal Ammount { get; set; }
        public TransactionType Type { get; set; }

        public UpdatedAccountBalance(Guid accountId, decimal ammount, TransactionType type) : base(DateTime.Now)
        {
            AccountId = accountId;
            Ammount = ammount;
            Type = type;
        }
    }
}
