using AutoMapper;
using MarketNet.src.Application.Categories.Dto;
using MarketNet.src.Domain.Entities.Products;
using MarketNet.src.Domain.Exceptions.Categories;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Categories.Commands
{
    public record CreateCategoryCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public CategoryDto? ParentCategory { get; set; }
        public ICollection<CategoryDto> ChildCategories { get; set; } = new List<CategoryDto>();
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
                foreach (CategoryDto categoryDto in request.ChildCategories)
                {
                    Category category = await categoryRepository.SearchById(categoryDto.Id);
                    if (category != null)
                    {
                        newCategories.Add(category);
                    }
                }
            }
            Category newCategory = new Category
            {
                Slug = request.Slug,
                Name = request.Name,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId,
                ChildCategories = newCategories
            };
            long id = await categoryRepository.AddAsync(newCategory);
            return id;

        }

    }
}
