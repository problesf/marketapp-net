using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Categories.Commands
{
    public record UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public CategoryBriefDto ParentCategory { get; set; }
        public ICollection<CategoryChildDto> ChildCategories { get; set; } = new List<CategoryChildDto>();
    }

    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            Category parent = await categoryRepository.GetByIdAsync(request.Id);
            if (parent == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            if (request.Name != null)
            {
                parent.Name = request.Name;
            }

            if (request.ParentCategoryId.HasValue)
            {
                parent.ParentCategoryId = request.ParentCategoryId.Value;
            }

            if (request.Description != null)
            {
                parent.Description = request.Description;
            }
            if (request.ChildCategories is { Count: > 0 })
            {
                CategorySearchCriteria categorySearchCriteria = new CategorySearchCriteria { ParentCategoryId = request.Id };
                IEnumerable<Category> categoriasAnteriores = await categoryRepository.Search(categorySearchCriteria);
                List<long> childCreated = request.ChildCategories.Where(c => c.Id > 0).Select(c => c.Id).ToList();
                List<CategoryChildDto> childNotCreated = request.ChildCategories.Where(c => c.Id == 0).ToList();
                List<Category> childtoCreate = mapper.Map<List<Category>>(childNotCreated, opt => opt.Items["parent"] = parent);
                List<long> childCreatedAndLinked = new List<long>();
                foreach (var item in categoriasAnteriores)
                {
                    bool seMantiene = childCreated.Contains(item.Id.Value);
                    if (!seMantiene)
                    {
                        item.ParentCategory = null;
                        item.ParentCategoryId = null;
                    }
                    else
                    {
                        childCreatedAndLinked.Add(item.Id.Value);
                    }
                }
                List<long> childCreatedAndNotLinked = new List<long>();

                categoryRepository.AddRangeAsync(childtoCreate);
                List<long> childCreatedToAdd = childCreated.Where(c => !childCreatedAndLinked.Contains(c)).ToList();
                foreach (var id in childCreatedToAdd)
                {
                    Category categoryNotLinked = await categoryRepository.SearchById(id);
                    if (categoryNotLinked == null)
                    {
                        throw new CategoryNotFoundException(id);
                    }
                    categoryNotLinked.ParentCategory = parent;
                }

            }


            await categoryRepository.SaveAsync(cancellationToken);


            return mapper.Map<CategoryDto>(await categoryRepository.GetByIdAsync(request.Id));

        }

    }

}
