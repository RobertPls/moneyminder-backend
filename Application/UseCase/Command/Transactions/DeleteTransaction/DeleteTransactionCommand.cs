using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Transactions.DeleteTransaction
{
    public record DeleteTransactionCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid TransactionId { get; set; }

        public DeleteTransactionCommand(Guid transactionId)
        {

            TransactionId = transactionId;
        }
    }
}
