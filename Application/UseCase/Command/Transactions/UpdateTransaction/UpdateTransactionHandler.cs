using Application.Utils;
using Domain.Factories.Transactions;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using Domain.Repositories.Categories;
using Domain.Repositories.Transactions;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Transactions.UpdateTransaction
{
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, Result>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFactory _transactionFactory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransactionHandler(ITransactionFactory transactionFactory, ICategoryRepository categoryRepository,ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _transactionRepository = transactionRepository;
            _transactionFactory = transactionFactory;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {

            var userProfile = await _userProfileRepository.FindByIdAsync(request.UserId??Guid.Empty);
            if (userProfile == null) return new Result(false, "User not found");

            var transaction = await _transactionRepository.FindByIdAsync(request.TransactionId);
            if (transaction == null) return new Result(false, "User Transaction not found");

            var accountOfTransaction = await _accountRepository.FindByIdAsync(transaction.AccountId);
            if (accountOfTransaction == null) return new Result(false, "Account of transaction not found");
            if (accountOfTransaction.UserProfileId != userProfile.Id) return new Result(false, "The user is not the owner of this transaction");

            var originAccount = await _accountRepository.FindByIdAsync(request.OriginAccountId);
            if (originAccount == null) return new Result(false, "Origin Account not found");
            if (originAccount.UserProfileId != userProfile.Id) return new Result(false, "The user is not the owner of this Origin Account");



            if (transaction.RelatedTransactionId == null && request.DestinationAccountId == null)
            {
                return await UpdateSimpleTransaction(request, transaction, userProfile.Id);          
            }
            else if(transaction.RelatedTransactionId == null && request.DestinationAccountId != null) 
            {
                return await UpdateTransactionToTransfer(request, transaction, userProfile.Id);
            }
            else if (transaction.RelatedTransactionId != null && request.DestinationAccountId != null)
            {
                return await UpdateTransfer(request, transaction, userProfile.Id);
            }
            else if (transaction.RelatedTransactionId != null && request.DestinationAccountId == null)
            {
                return await UpdateTransferToTransaction(request, transaction, userProfile.Id);
            }
            else
            {
                return new Result(false, "Parameter Error");
            }
        }


        public async Task<Result> UpdateSimpleTransaction(UpdateTransactionCommand request, Transaction transaction, Guid userProfile)
        {

            var newCategory = await _categoryRepository.FindByIdAsync(request.CategoryId ?? Guid.Empty);
            if (newCategory == null) return new Result(false, "Update Simple transaction need categoryId");
            if (newCategory.UserProfileId != userProfile) return new Result(false, "The user is not the owner of this category");

            var oldType = transaction.Type;
            decimal oldAmount = transaction.Amount;
            Guid oldAccountId = transaction.AccountId;

            var newtype = request.Type == Dto.Transactions.TransactionType.Income ? TransactionType.Income : TransactionType.Outcome;

            transaction.UpdateType(newtype);
            transaction.UpdateDate(request.Date);
            transaction.UpdateDescription(request.Description);
            transaction.UpdateAmout(request.Amount);
            transaction.UpdateCategory(newCategory.Id);
            transaction.UpdateAccount(request.OriginAccountId);
            transaction.Updated(oldAccountId,transaction.AccountId,oldAmount,transaction.Amount,oldType,transaction.Type);

            await _transactionRepository.UpdateAsync(transaction);

            await _unitOfWork.Commit();

            return new Result(true, "Transaction has been Updated");
        }

        public async Task<Result> UpdateTransactionToTransfer(UpdateTransactionCommand request, Transaction transaction, Guid userProfile)
        {
            if (request.OriginAccountId == request.DestinationAccountId)
            {
                return new Result(false, "The origin account is the same as the destination account.");
            }

            var destinationAccount = await _accountRepository.FindByIdAsync(request.DestinationAccountId ?? Guid.Empty);
            if (destinationAccount == null) return new Result(false, "Destination Account not found");
            if (destinationAccount.UserProfileId != userProfile) return new Result(false, "The user is not the owner of this destination Account");

            var transactionInDestinationAccount = _transactionFactory.CreateIncomeTransaction(destinationAccount.Id, null, request.Date, request.Amount, "Transference");
            
            transactionInDestinationAccount.AddTransacionRelation(transaction.Id);

            await _transactionRepository.CreateAsync(transactionInDestinationAccount);

            await _unitOfWork.Commit();

            var oldType = transaction.Type;
            decimal oldAmount = transaction.Amount;
            Guid oldAccountId = transaction.AccountId;

            var newtype = TransactionType.Outcome;

            transaction.AddTransacionRelation(transactionInDestinationAccount.Id);
            transaction.UpdateType(newtype);
            transaction.UpdateDate(request.Date);
            transaction.UpdateDescription(request.Description);
            transaction.UpdateAmout(request.Amount);
            transaction.RemoveCategoryRelation();
            transaction.UpdateAccount(request.OriginAccountId);
            transaction.Updated(oldAccountId,transaction.AccountId,oldAmount,transaction.Amount,oldType,transaction.Type);

            await _transactionRepository.UpdateAsync(transaction);

            await _unitOfWork.Commit();

            return new Result(true, "Transaction updated to transfer successfully.");
        }

        public async Task<Result> UpdateTransferToTransaction(UpdateTransactionCommand request, Transaction transaction, Guid userProfile)
        {

            var newCategory = await _categoryRepository.FindByIdAsync(request.CategoryId ?? Guid.Empty);
            if (newCategory == null) return new Result(false, "Update Transference to transaction need categoryId");
            if (newCategory.UserProfileId != userProfile) return new Result(false, "The user is not the owner of this category");

            var relatedTransaction = await _transactionRepository.FindByIdAsync(transaction.RelatedTransactionId??Guid.Empty);
            if (relatedTransaction == null) return new Result(false, "RelatedTransaction not found");

            transaction.RemoveTransacionRelation();
            relatedTransaction.RemoveTransacionRelation();

            await _transactionRepository.UpdateAsync(transaction);
            await _transactionRepository.UpdateAsync(relatedTransaction);

            await _unitOfWork.Commit();

            await _transactionRepository.RemoveAsync(relatedTransaction);

            var oldType = transaction.Type;
            decimal oldAmount = transaction.Amount;
            Guid oldAccountId = transaction.AccountId;

            var newtype = request.Type == Dto.Transactions.TransactionType.Income ? TransactionType.Income : TransactionType.Outcome;

            transaction.UpdateType(newtype);
            transaction.UpdateDate(request.Date);
            transaction.UpdateDescription(request.Description);
            transaction.UpdateAmout(request.Amount);
            transaction.UpdateCategory(newCategory.Id);
            transaction.UpdateAccount(request.OriginAccountId);
            transaction.Updated(oldAccountId, transaction.AccountId, oldAmount, transaction.Amount, oldType, transaction.Type);

            await _transactionRepository.UpdateAsync(transaction);

            await _unitOfWork.Commit();

            return new Result(true, "Transfer updated to transaction successfully");
        }
        public async Task<Result> UpdateTransfer(UpdateTransactionCommand request, Transaction transaction, Guid userProfile)
        {
            if (request.OriginAccountId == request.DestinationAccountId)
            {
                return new Result(false, "The origin account is the same as the destination account.");
            }

            var destinationAccount = await _accountRepository.FindByIdAsync(request.DestinationAccountId ?? Guid.Empty);
            if (destinationAccount == null) return new Result(false, "Destination Account not found");
            if (destinationAccount.UserProfileId != userProfile) return new Result(false, "The user is not the owner of this destination Account");

            var relatedTransaction = await _transactionRepository.FindByIdAsync(transaction.RelatedTransactionId ?? Guid.Empty);
            if (relatedTransaction == null) return new Result(false, "RelatedTransaction not found");


            var oldType = transaction.Type;
            decimal oldAmount = transaction.Amount;
            Guid oldAccountId = transaction.AccountId;

            var newtype = TransactionType.Outcome;

            transaction.UpdateType(newtype);
            transaction.UpdateDate(request.Date);
            transaction.UpdateDescription("Transference");
            transaction.UpdateAmout(request.Amount);
            transaction.RemoveCategoryRelation();
            transaction.UpdateAccount(request.OriginAccountId);
            transaction.Updated(oldAccountId, transaction.AccountId, oldAmount, transaction.Amount, oldType, transaction.Type);

            await _transactionRepository.UpdateAsync(transaction);


            var oldTypeRelatedTransaction = relatedTransaction.Type;
            decimal oldAmountRelatedTransaction = relatedTransaction.Amount;
            Guid oldAccountIdRelatedTransaction = relatedTransaction.AccountId;

            var newtypeRelatedTransaction = TransactionType.Income;

            relatedTransaction.UpdateType(newtypeRelatedTransaction);
            relatedTransaction.UpdateDate(request.Date);
            relatedTransaction.UpdateDescription("Transference");
            relatedTransaction.UpdateAmout(request.Amount);
            relatedTransaction.RemoveCategoryRelation();
            relatedTransaction.UpdateAccount(destinationAccount.Id);
            relatedTransaction.Updated(oldAccountIdRelatedTransaction, relatedTransaction.AccountId, oldAmountRelatedTransaction, relatedTransaction.Amount, oldTypeRelatedTransaction, relatedTransaction.Type);

            await _transactionRepository.UpdateAsync(relatedTransaction);

            await _unitOfWork.Commit();

            return new Result(true, "Transfer updated successfully");
        }
                    
    }
}
