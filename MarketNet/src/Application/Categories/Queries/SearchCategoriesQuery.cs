using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Categories.Queries
{
    public record SearchCategoriesQuery : IRequest<List<CategoryDto>>
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public long? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }


    }

    public class SearchCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<SearchCategoriesQuery, List<CategoryDto>>
    {
        public async Task<List<CategoryDto>> Handle(SearchCategoriesQuery request, CancellationToken cancellationToken)
        {
            CategorySearchCriteria criteria = new()
            {
                Name = request.Name,
                Description = request.Description,
                Slug = request.Slug,
                ParentCategoryId = request.ParentCategoryId,
                Id = request.Id,
                IsActive = request.IsActive
            };

            var categories = await categoryRepository.Search(criteria);
            return mapper.Map<List<CategoryDto>>(categories);

        }

    }
}
