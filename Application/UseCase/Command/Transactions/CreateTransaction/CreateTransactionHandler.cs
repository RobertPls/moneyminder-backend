using Application.Utils;
using Domain.Factories.Transactions;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using Domain.Repositories.Categories;
using Domain.Repositories.UserProfiles;
using Domain.Repositories.Transactions;
using MediatR;
using SharedKernel.Core;
using Domain.Models.UserProfiles;

namespace Application.UseCase.Command.Transactions.CreateTransaction
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Result>
    {
        private readonly IUserProfileRepository _userUserProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionHandler(
            ITransactionRepository transactionRepository,
            IUserProfileRepository userUserProfileRepository,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            ITransactionFactory transactionFactory, 
            IUnitOfWork unitOfWort)
        {
            _transactionRepository = transactionRepository;
            _userUserProfileRepository = userUserProfileRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _transactionFactory = transactionFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userUserProfileRepository.FindByUserIdAsync(request.UserId!.Value);
            if (userProfile == null) return new Result(false, "User not found");

            var account = await _accountRepository.FindByIdAsync(request.AccountId);
            if (account == null)return new Result(false, "Account not found");
            if (account.UserProfileId != userProfile.Id)return new Result(false, "The user is not the owner of this Account");            

            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null) return new Result(false, "Category not found");        
            if (category.UserProfileId != userProfile.Id)return new Result(false, "The user is not the owner of this Category");
            

            Transaction transaction = request.Type == Dto.Transactions.TransactionType.Income ?
                _transactionFactory.CreateIncomeTransaction(request.AccountId, request.CategoryId, request.Date, request.Amount, request.Description) :
                _transactionFactory.CreateOutcomeTransaction(request.AccountId, request.CategoryId, request.Date, request.Amount, request.Description);

            await _transactionRepository.CreateAsync(transaction);

            await _unitOfWork.Commit();

            return new Result(true, "Transaction created successfully.");

        }
    }
}
