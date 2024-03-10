using MediatR;

namespace UniSync.Application.Features.Categories.Queries.GetById
{
    public record GetByIdCategoryQuery(Guid Id) : IRequest<CategoryDto>; 
}
