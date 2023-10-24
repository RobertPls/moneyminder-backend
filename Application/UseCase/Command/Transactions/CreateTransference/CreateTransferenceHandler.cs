using Application.UseCase.Command.Transactions.CreateTransference;
using Application.Utils;
using Domain.Factories.Transactions;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using Domain.Repositories.Categories;
using Domain.Repositories.Transactions;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Transferences.CreateTransference
{
    public class CreateTransferenceHandler : IRequestHandler<CreateTransferenceCommand, Result>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransferenceHandler(
            ITransactionRepository transactionRepository,
            IUserProfileRepository userProfileRepository,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            ITransactionFactory transactionFactory,
            IUnitOfWork unitOfWort)
        {
            _transactionRepository = transactionRepository;
            _userProfileRepository = userProfileRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _transactionFactory = transactionFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(CreateTransferenceCommand request, CancellationToken cancellationToken)
        {

            var user = await _userProfileRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");


            if (request.OriginAccountId == request.DestinationAccountId)return new Result(false, "The origin account is the same as the destination account.");            

            var originAccount = await _accountRepository.FindByIdAsync(request.OriginAccountId);
            var destinationAccount = await _accountRepository.FindByIdAsync(request.DestinationAccountId);

            if (originAccount == null || destinationAccount == null)return new Result(false, "Account not found");           

            if (originAccount.UserProfileId != user.Id || destinationAccount.UserProfileId != user.Id) return new Result(false, "The user is not the owner of this Account");           

            Transaction transactionInOriginAccount = _transactionFactory.CreateOutcomeTransaction(request.OriginAccountId, null, request.Date, request.Amount, "Transference");

            Transaction transactionInDestinationAccount = _transactionFactory.CreateIncomeTransaction(request.DestinationAccountId, null, request.Date, request.Amount, "Transference");

            transactionInOriginAccount.AddTransacionRelation(transactionInDestinationAccount.Id);

            transactionInDestinationAccount.AddTransacionRelation(transactionInOriginAccount.Id);

            await _transactionRepository.CreateAsync(transactionInOriginAccount);

            await _transactionRepository.CreateAsync(transactionInDestinationAccount);

            await _unitOfWork.Commit();
      
            return new Result(true, "Transference created successfully.");

        }
    }
}
