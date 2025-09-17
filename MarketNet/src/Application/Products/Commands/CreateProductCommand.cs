using AutoMapper;
using MarketNet.Application.Common.Interfaces;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Persistence.Repositories;
using MarketNet.src.Application.Products.Dto;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record CreateProductCommand : IRequest<long>
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public decimal TaxRate { get; init; }
        public string Currency { get; init; }

        public List<long>? CategoriesId { get; set; }

        public List<PAttributeDto>? Attributes { get; set; }
    }

    public class CreateProductCommandHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper, IUserContext userContext) : IRequestHandler<CreateProductCommand, long>
    {

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var sellerId = userContext.UserId ?? throw new UnauthorizedAccessException();

            Product exist = await productRepository.SearchByProductCode(request.Code);
            if (exist != null)
            {
                throw new ProductExistException($"Ya existe un product con c�digo {request.Code}.");
            }

            List<Category> productCategories = new List<Category>();
            if (request.CategoriesId != null)
            {
                foreach (long idCategory in request.CategoriesId)
                {
                    Category category = await categoryRepository.SearchById(idCategory);
                    if (category == null)
                    {
                        throw new CategoryNotFoundException($"Categoria con ID {idCategory} no encontrado");
                    }
                    productCategories.Add(category);
                }
            }
            List<PAttribute> attributes = null;
            if (request.Attributes != null)
                attributes = mapper.Map<List<PAttribute>>(request.Attributes);
            Product newProduct = new Product(
                request.Code,
                request.Name,
                request.Description,
                request.Price,
                request.Stock,
                request.TaxRate,
                request.Currency,
                true,
                sellerId,
                productCategories,
                attributes
            );
            newProduct.Categories = productCategories;
            await productRepository.AddAsync(newProduct);
            await productRepository.SaveAsync(cancellationToken);
            return newProduct.Id.Value;

        }

    }
}
