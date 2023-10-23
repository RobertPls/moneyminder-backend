using Domain.Events.Users;
using Domain.Factories.Accounts;
using Domain.Repositories.Accounts;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Consumers.Accounts
{
    public class CreateFirstUserAccountHandler : INotificationHandler<CreatedUser>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFirstUserAccountHandler(IAccountRepository accountRepository, IAccountFactory accountFactory, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _accountFactory = accountFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(CreatedUser notification, CancellationToken cancellationToken)
        {
            var account = _accountFactory.Create(notification.UserId, "Billetera", "Dinero Fisico");
            await _accountRepository.CreateAsync(account);
        }
    }
}
