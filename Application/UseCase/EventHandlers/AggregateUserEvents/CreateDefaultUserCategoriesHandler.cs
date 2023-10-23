using Domain.Events.Users;
using Domain.Factories.Categories;
using Domain.Repositories.Categories;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.EventHandlers.AggregateUserEvents
{
    public class CreateDefaultUserCategoriesHandler : INotificationHandler<CreatedUser>
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

        public async Task Handle(CreatedUser notification, CancellationToken cancellationToken)
        {
            var categoryAdjustment = _categoryFactory.Create(notification.UserId, "Ajuste", true);
            var categoryEntertainment = _categoryFactory.Create(notification.UserId, "Entretenimiento", true);
            var categoryFood = _categoryFactory.Create(notification.UserId, "Alimentacion", true);
            var categoryTransport = _categoryFactory.Create(notification.UserId, "Transporte", true);
            var categoryHealth = _categoryFactory.Create(notification.UserId, "Salud", true);
            var categoryTravel = _categoryFactory.Create(notification.UserId, "Viaje", true);

            await _categoryRepository.CreateAsync(categoryAdjustment);
            await _categoryRepository.CreateAsync(categoryEntertainment);
            await _categoryRepository.CreateAsync(categoryFood);
            await _categoryRepository.CreateAsync(categoryTransport);
            await _categoryRepository.CreateAsync(categoryHealth);
            await _categoryRepository.CreateAsync(categoryTravel);
        }
    }
}
