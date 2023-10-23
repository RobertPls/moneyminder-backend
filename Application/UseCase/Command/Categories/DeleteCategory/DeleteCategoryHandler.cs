using Application.Utils;
using Domain.Models.Categories;
using Domain.Models.Users;
using Domain.Repositories.Categories;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Categories.DeleteCategory
{

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IUserRepository userRepository, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");            

            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null)return new Result(false, "User Category not found");           
            if (category.UserId != request.UserId)return new Result(false, "The user is not the owner of this category");          
            if (category.IsDefault)return new Result(false, "Cant delete default category");         

            await _categoryRepository.RemoveAsync(category);

            await _unitOfWork.Commit();

            return new Result(true, "Category has deleted");

        }
    }
}
