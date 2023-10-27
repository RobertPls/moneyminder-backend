﻿using Domain.Models.Transactions;
using SharedKernel.Core;

namespace Domain.Events.Transactions
{
    public record CreatedTransaction : DomainEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public bool IsTransfer { get; set; }


        public CreatedTransaction( Guid accountId, decimal amount, TransactionType type, bool isTransfer) : base(DateTime.Now)
        {
            AccountId = accountId;
            Amount = amount;
            Type = type;
            IsTransfer = isTransfer;
        }
    }
}
