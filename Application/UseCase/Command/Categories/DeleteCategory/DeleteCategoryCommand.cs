using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Categories.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid CategoryId { get; set; }

        public DeleteCategoryCommand(Guid categoryId)
        {

            CategoryId = categoryId;
        }
    }
}
