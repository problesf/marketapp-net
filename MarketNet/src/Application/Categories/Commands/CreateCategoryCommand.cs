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

            if (request.ParentCategoryId != null)
            {
                Category existParent = await categoryRepository.SearchById(request.ParentCategoryId.Value);
                if (existParent == null)
                {
                    throw new CategoryExistException(request.ParentCategoryId.Value);
                }
            }

            Category parentCategory = new Category(
                request.Name,
                request.Slug,
                request.Description,
                request.ParentCategoryId,
                null
            );
            await categoryRepository.AddAsync(parentCategory);


            if (request.ChildCategories is { Count: > 0 })
            {
                foreach (var item in request.ChildCategories)
                {
                    if (item.Id > 0)
                    {
                        Category category = await categoryRepository.GetByIdAsync(item.Id);
                        category.ParentCategory = parentCategory;
                    }
                    else
                    {
                        Category newCategory = mapper.Map<Category>(item);
                        newCategory.Id = null;
                        newCategory.ParentCategory = parentCategory;
                        await categoryRepository.AddAsync(newCategory);
                    }
                }
            }
            await categoryRepository.SaveAsync(cancellationToken);
            return parentCategory.Id.Value;

        }

    }
}