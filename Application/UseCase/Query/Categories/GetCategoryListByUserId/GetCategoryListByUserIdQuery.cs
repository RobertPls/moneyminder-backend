using Application.Dto.Accounts;
using Application.Dto.Categories;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Query.Categories.GetCategoryListByUserId
{
    public class GetCategoryListByUserIdQuery : IRequest<Result<IEnumerable<CategoryDto>>>
    {
        public Guid UserId { get; set; }

        public GetCategoryListByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
