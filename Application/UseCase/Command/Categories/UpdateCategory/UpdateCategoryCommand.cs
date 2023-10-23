using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Categories.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public UpdateCategoryCommand(Guid categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
    }
}
