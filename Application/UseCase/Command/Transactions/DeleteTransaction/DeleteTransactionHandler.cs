using Application.Utils;
using Domain.Events.Transactions;
using Domain.Models.Accounts;
using Domain.Models.Transactions;
using Domain.Models.Users;
using Domain.Repositories.Accounts;
using Domain.Repositories.Transactions;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Transactions.DeleteTransaction
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserRepository userRepository, IUnitOfWork unitOfWort)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");

            var transaction = await _transactionRepository.FindByIdAsync(request.TransactionId);
            if (transaction == null)return new Result(false, "User Transaction not found");
           
            var account = await _accountRepository.FindByIdAsync(transaction.AccountId);
            if (account == null)return new Result(false, "User Acccount not found");           
            if (account.UserId != request.UserId)return new Result(false, "The user is not the owner of this account");
            

            var relatedTransactionId = transaction.RelatedTransactionId;

            if (relatedTransactionId != null)
            {
                var relatedTransaction = await _transactionRepository.FindByIdAsync(relatedTransactionId.Value);
                if (relatedTransaction == null)return new Result(false, "Related Transaction not found");                

                relatedTransaction.RemoveTransacionRelation();
                
                transaction.RemoveTransacionRelation();

                await _transactionRepository.UpdateAsync(relatedTransaction);

                await _transactionRepository.UpdateAsync(transaction);

                await _unitOfWork.Commit();

                await _transactionRepository.RemoveAsync(relatedTransaction);
            }

            await _transactionRepository.RemoveAsync(transaction);
           
            await _unitOfWork.Commit();

            return new Result(true, "Transaction has been deleted");

        }
    }
}
