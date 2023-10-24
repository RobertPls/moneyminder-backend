using Application.Utils;
using Domain.Repositories.Categories;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Categories.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.FindByUserIdAsync(request.UserId!.Value);
            if (userProfile == null)return new Result(false, "User not found");          

            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null)return new Result(false, "User Category not found");           
            if (category.UserProfileId != userProfile.Id)return new Result(false, "The user is not the owner of this category");

            var existingCategory = await _categoryRepository.FindByNameAsync(userProfile.Id, request.Name);
            if (existingCategory != null) return new Result(false, "A category with the same name already exists.");

            category.UpdateName(request.Name);

            await _categoryRepository.UpdateAsync(category);

            await _unitOfWork.Commit();

            return new Result<string>(true, "Category has updated");

        }
    }
}
