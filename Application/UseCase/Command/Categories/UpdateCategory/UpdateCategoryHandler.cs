using Application.Utils;
using Domain.Models.Categories;
using Domain.Models.Users;
using Domain.Repositories.Categories;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Categories.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IUserRepository userRepository, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null)return new Result(false, "User not found");          

            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null)return new Result(false, "User Category not found");           
            if (category.UserId != request.UserId)return new Result(false, "The user is not the owner of this category");           

            category.UpdateName(request.Name);

            await _categoryRepository.UpdateAsync(category);

            await _unitOfWork.Commit();

            return new Result<string>(true, "Category has updated");

        }
    }
}
