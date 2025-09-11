using AutoMapper;
using MarketNet.src.Domain.Entities.Products;
using MarketNet.src.Domain.Exceptions.Categories;
using MarketNet.src.Domain.Exceptions.Products;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Products.Commands
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

        public List<long> categoriesId { get; set; }
    }

    public class CreateProductCommandHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper) : IRequestHandler<CreateProductCommand, long>
    {

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product exist = await productRepository.SearchByProductCode(request.Code);
            if (exist != null)
            {
                throw new ProductExistException($"Ya existe un product con código {request.Code}.");
            }

            ICollection<Category> productCategories = new List<Category>();

            foreach (long idCategory in request.categoriesId)
            {
                Category category = await categoryRepository.GetByIdAsync(idCategory);
                if (category == null)
                {
                    throw new CategoryNotFoundException($"Categoria con ID {idCategory} no encontrado");
                }
                productCategories.Add(category);
            }
            Product newProduct = new Product
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                TaxRate = request.TaxRate,
                Stock = request.Stock,
                Currency = request.Currency,
                Categories = productCategories,
                IsActive = true
            };

            long id = await productRepository.AddAsync(newProduct);
            return id;

        }

    }
}
