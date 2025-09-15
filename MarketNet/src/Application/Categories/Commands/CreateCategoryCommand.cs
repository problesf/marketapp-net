using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Categories.Commands
{
    public record CreateCategoryCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public CategoryBriefDto? ParentCategory { get; set; }
        public ICollection<CategoryChildDto> ChildCategories { get; set; } = new List<CategoryChildDto>();

    }

    public class CreateCategoryCommandCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<CreateCategoryCommand, long>
    {

        public async Task<long> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category exist = await categoryRepository.SearchBySlug(request.Slug);
            if (exist != null)
            {
                throw new CategoryExistException($"Ya existe una categoria con slug {request.Slug}.");
            }

            List<Category> newCategories = [];
            if (request.ChildCategories != null && request.ChildCategories.Any())
            {
                foreach (CategoryChildDto categoryDto in request.ChildCategories)
                {
                    Category category = await categoryRepository.SearchById(categoryDto.Id);
                    if (category != null)
                    {
                        newCategories.Add(category);
                    }
                }
            }
            Category newCategory = new Category(
                request.Name,
                request.Slug,
                request.Description,
                request.ParentCategoryId
            );
            await categoryRepository.AddAsync(newCategory);
            await categoryRepository.SaveAsync(cancellationToken);
            return newCategory.Id.Value;

        }

    }
}
