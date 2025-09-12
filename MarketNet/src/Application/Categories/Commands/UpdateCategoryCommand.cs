using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.Application.Categories.Commands
{
    public record UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public virtual CategoryDto ParentCategory { get; set; }
        public virtual ICollection<CategoryDto> ChildCategories { get; set; } = new List<CategoryDto>();
    }

    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            Category category = await categoryRepository.SearchBySlug(request.Slug);
            if (category == null)
            {
                throw new CategoryNotFoundException($"Categoria con slug {request.Slug}");
            }

            if (request.Name != null)
            {
                category.Name = request.Name;
            }

            if (request.ParentCategoryId.HasValue)
            {
                category.ParentCategoryId = request.ParentCategoryId.Value;
            }

            if (request.Description != null)
            {
                category.Description = request.Description;
            }


            await categoryRepository.UpdateAsync(category);
            return mapper.Map<CategoryDto>(category);

        }

    }

}
