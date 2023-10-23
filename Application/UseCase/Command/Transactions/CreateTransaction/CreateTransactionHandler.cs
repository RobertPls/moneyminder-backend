using Application.Utils;
using Domain.Factories.Transactions;
using Domain.Models.Accounts;
using Domain.Models.Categories;
using Domain.Models.Transactions;
using Domain.Models.Users;
using Domain.Repositories.Accounts;
using Domain.Repositories.Categories;
using Domain.Repositories.Transactions;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Transactions.CreateTransaction
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionHandler(
            ITransactionRepository transactionRepository,
            IUserRepository userRepository,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            ITransactionFactory transactionFactory, 
            IUnitOfWork unitOfWort)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _transactionFactory = transactionFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");

            var account = await _accountRepository.FindByIdAsync(request.AccountId);
            if (account == null)return new Result(false, "Account not found");
            if (account.UserId != user.Id)return new Result(false, "The user is not the owner of this Account");            

            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null) return new Result(false, "Category not found");        
            if (category.UserId != user.Id)return new Result(false, "The user is not the owner of this Category");
            

            Transaction transaction = request.Type == Dto.Transactions.TransactionType.Income ?
                _transactionFactory.CreateIncomeTransaction(request.AccountId, request.CategoryId, request.Date, request.Amount, request.Description) :
                _transactionFactory.CreateOutcomeTransaction(request.AccountId, request.CategoryId, request.Date, request.Amount, request.Description);

            await _transactionRepository.CreateAsync(transaction);

            await _unitOfWork.Commit();

            return new Result(true, "Transaction created successfully.");

        }
    }
}
