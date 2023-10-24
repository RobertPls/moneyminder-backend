using Domain.Events.UserProfiles;
using Domain.Factories.Accounts;
using Domain.Repositories.Accounts;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Consumers.Accounts
{
    public class CreateFirstUserAccountHandler : INotificationHandler<CreatedUserProfile>
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

        public async Task Handle(CreatedUserProfile notification, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine("asdasdasd+asdasdasd+asd+asd+as+das+das+da+sd+asd+asd" + notification.UserProfileId);

            var account = _accountFactory.Create(notification.UserProfileId, "Billetera", "Dinero Fisico");
            await _accountRepository.CreateAsync(account);

            await _unitOfWork.Commit();
        }
    }
}
