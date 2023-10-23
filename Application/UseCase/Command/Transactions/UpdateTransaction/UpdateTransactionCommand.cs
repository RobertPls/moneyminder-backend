using Application.Dto.Transactions;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Transactions.UpdateTransaction
{
    public record UpdateTransactionCommand : IRequest<Result>
    {
        public Guid TransactionId { get; set; }
        public Guid? UserId { get; set; }
        public Guid OriginAccountId { get; set; }
        public Guid? DestinationAccountId { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType? Type { get; set; }
        public UpdateTransactionCommand(Guid transactionId, Guid originAccountId, Guid? destinationAccountId, Guid? categoryId, DateTime date, decimal amount, string description, TransactionType? type)
        {
            TransactionId = transactionId;
            OriginAccountId = originAccountId;
            DestinationAccountId = destinationAccountId;
            CategoryId = categoryId;
            Date = date;
            Amount = amount;
            Description = description;
            Type = type;
        }
    }
}
