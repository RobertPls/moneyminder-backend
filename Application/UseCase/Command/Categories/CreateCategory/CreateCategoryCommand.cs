using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Categories.CreateCategory
{
    public record CreateCategoryCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public string Name { get; set; }

        public CreateCategoryCommand(string name)
        {
            Name = name;
        }
    }
}
