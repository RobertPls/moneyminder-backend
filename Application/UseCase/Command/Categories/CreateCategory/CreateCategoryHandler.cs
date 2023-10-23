using Application.Utils;
using Domain.Factories.Categories;
using Domain.Models.Categories;
using Domain.Models.Users;
using Domain.Repositories.Categories;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Categories.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryFactory _categoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IUserRepository userRepository, ICategoryFactory categoryFactory, IUnitOfWork unitOfWort)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _categoryFactory = categoryFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");

            var existingCategory = await _categoryRepository.FindByNameAsync(user.Id, request.Name);
            if (existingCategory != null) return new Result(false, "A category with the same name already exists.");

            var category = _categoryFactory.Create(userId: request.UserId!.Value,name: request.Name,isDefault: false);

            await _categoryRepository.CreateAsync(category);

            await _unitOfWork.Commit();

            return new Result(true, "Category created successfully.");

        }
    }
}
