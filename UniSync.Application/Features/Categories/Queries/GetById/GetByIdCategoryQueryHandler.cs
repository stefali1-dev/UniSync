using UniSync.Application.Persistence;
using MediatR;

namespace UniSync.Application.Features.Categories.Queries.GetById
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryDto>
    {
        private readonly ICategoryRepository repository;

        public GetByIdCategoryQueryHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }
        public async Task<CategoryDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await repository.FindByIdAsync(request.Id);
            if (category.IsSuccess)
            {
                return new CategoryDto
                {
                    CategoryId = category.Value.CategoryId,
                    CategoryName = category.Value.CategoryName
                };
            }
            return new CategoryDto();
        }
    }
}
