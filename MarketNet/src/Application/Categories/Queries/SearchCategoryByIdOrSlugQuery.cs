using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Categories.Queries
{
    public record SearchCategoryByIdOrSlugQuery : IRequest<CategoryDto>
    {
        public long? Id { get; set; }

        public string? Slug { get; set; }

    }

    public class SearchCategoryByIdOrSlugQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<SearchCategoryByIdOrSlugQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(SearchCategoryByIdOrSlugQuery request, CancellationToken cancellationToken)
        {
            Category category;

            if (!string.IsNullOrWhiteSpace(request.Slug))
            {
                category = await categoryRepository.SearchBySlug(request.Slug);
                if (category == null)
                    throw new CategoryNotFoundException($"Categoria con slug '{request.Slug}' no encontrado");
            }
            else if (request.Id.HasValue)
            {
                category = await categoryRepository.SearchById(request.Id.Value);
                if (category == null)
                    throw new CategoryNotFoundException($"Categoria con ID {request.Id.Value} no encontrado");
            }
            else
            {
                throw new ArgumentException("Debes proporcionar al menos C�digo o Slug de la categoria");
            }


            return mapper.Map<CategoryDto>(category);

        }

    }
}
