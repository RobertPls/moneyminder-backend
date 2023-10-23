using Application.Dto.Categories;
using Application.UseCase.Query.Categories.GetCategoryListByUserId;
using Application.Utils;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.ReadModel.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query.Categories.GetCategoryListByUserId
{
    internal class GetCategoryListByUserIdHandler : IRequestHandler<GetCategoryListByUserIdQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly DbSet<CategoryReadModel> category;
        public GetCategoryListByUserIdHandler(ReadDbContext dbContext)
        {
            category = dbContext.Category;
        }
        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetCategoryListByUserIdQuery request, CancellationToken cancellationToken)
        {
            var query = category.Where(x => x.UserId == request.UserId).AsNoTracking().AsQueryable();

            var list = await query.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return new Result<IEnumerable<CategoryDto>>(list, true, "User category list");
        }
    }
}
