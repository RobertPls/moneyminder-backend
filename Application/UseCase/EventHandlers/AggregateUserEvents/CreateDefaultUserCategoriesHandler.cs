using Domain.Events.UserProfiles;
using Domain.Factories.Categories;
using Domain.Repositories.Categories;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.EventHandlers.AggregateUserEvents
{
    public class CreateDefaultUserCategoriesHandler : INotificationHandler<CreatedUserProfile>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryFactory _categoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDefaultUserCategoriesHandler(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _categoryFactory = categoryFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(CreatedUserProfile notification, CancellationToken cancellationToken)
        {
            var categoryAdjustment = _categoryFactory.Create(notification.UserProfileId, "Ajuste", true);
            var categoryEntertainment = _categoryFactory.Create(notification.UserProfileId, "Entretenimiento", true);
            var categoryFood = _categoryFactory.Create(notification.UserProfileId, "Alimentacion", true);
            var categoryTransport = _categoryFactory.Create(notification.UserProfileId, "Transporte", true);
            var categoryHealth = _categoryFactory.Create(notification.UserProfileId, "Salud", true);
            var categoryTravel = _categoryFactory.Create(notification.UserProfileId, "Viaje", true);

            await _categoryRepository.CreateAsync(categoryAdjustment);
            await _categoryRepository.CreateAsync(categoryEntertainment);
            await _categoryRepository.CreateAsync(categoryFood);
            await _categoryRepository.CreateAsync(categoryTransport);
            await _categoryRepository.CreateAsync(categoryHealth);
            await _categoryRepository.CreateAsync(categoryTravel);

            await _unitOfWork.Commit();
        }
    }
}
