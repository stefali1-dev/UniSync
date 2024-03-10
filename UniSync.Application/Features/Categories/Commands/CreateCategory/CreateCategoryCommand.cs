using MediatR;

namespace UniSync.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
    {
        public string CategoryName { get; set; } = default!;
    }
}
