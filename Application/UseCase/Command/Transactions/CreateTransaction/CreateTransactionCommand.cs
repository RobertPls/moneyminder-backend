using Application.Dto.Transactions;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Transactions.CreateTransaction
{
    public record CreateTransactionCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }


        public CreateTransactionCommand(Guid accountId, Guid categoryId, DateTime date, decimal amount, string description, TransactionType type)
        {
            AccountId = accountId;
            CategoryId = categoryId;
            Date = date;
            Amount = amount;
            Description = description;
            Type = type;
        }
    }
}
