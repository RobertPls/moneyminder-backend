using Application.Utils;
using Domain.Repositories.Categories;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Categories.DeleteCategory
{

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.FindByIdAsync(request.UserId!.Value);
            if (userProfile == null) return new Result(false, "User not found");            

            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null)return new Result(false, "User Category not found");           
            if (category.UserProfileId != userProfile.Id)return new Result(false, "The user is not the owner of this category");          
            if (category.IsDefault)return new Result(false, "Cant delete default category");         

            await _categoryRepository.RemoveAsync(category);

            await _unitOfWork.Commit();

            return new Result(true, "Category has deleted");

        }
    }
}
