using Application.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Command.Transactions.CreateTransference
{
    public record CreateTransferenceCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid OriginAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }


        public CreateTransferenceCommand(Guid originAccountId, Guid destinationAccountId, DateTime date, decimal amount)
        {
            OriginAccountId = originAccountId;
            DestinationAccountId = destinationAccountId;    
            Date = date;
            Amount = amount;
        }
    }
}
