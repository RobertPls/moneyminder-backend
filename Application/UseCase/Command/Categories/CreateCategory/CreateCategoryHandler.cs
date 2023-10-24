using Application.Utils;
using Domain.Factories.Categories;
using Domain.Repositories.Categories;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Categories.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result>
    {
        private readonly IUserProfileRepository _userUserProfileRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryFactory _categoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IUserProfileRepository userUserProfileRepository, ICategoryFactory categoryFactory, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _userUserProfileRepository = userUserProfileRepository;
            _categoryFactory = categoryFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var userProfile = await _userUserProfileRepository.FindByUserIdAsync(request.UserId!.Value);
            if (userProfile == null) return new Result(false, "User not found");

            var existingCategory = await _categoryRepository.FindByNameAsync(userProfile.Id, request.Name);
            if (existingCategory != null) return new Result(false, "A category with the same name already exists.");

            var category = _categoryFactory.Create(userProfile.Id, request.Name, false);

            await _categoryRepository.CreateAsync(category);

            await _unitOfWork.Commit();

            return new Result(true, "Category created successfully.");

        }
    }
}
