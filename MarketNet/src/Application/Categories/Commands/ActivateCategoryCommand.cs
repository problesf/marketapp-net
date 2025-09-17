using AutoMapper;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Categories.Commands
{
    public record ActivateCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public bool IsActive { get; set; }


    }

    public class ActivateCategoryCommanddHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<ActivateCategoryCommand, bool>
    {

        public async Task<bool> Handle(ActivateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category exist = await categoryRepository.SearchById(request.Id);
            if (exist == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }
            exist.IsActive = request.IsActive;
            return await categoryRepository.SaveAsync(cancellationToken);



        }

    }
}