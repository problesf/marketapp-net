using AutoMapper;
using MarketApp.src.Domain.entities.product;
using MarketNet.src.Domain.Exceptions.Products;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Products.Queries
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
    }

    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand, long>
    {

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product exist = await productRepository.SearchByProductCode(request.Code);
            if (exist != null)
            {
                throw new ProductExistException($"Ya existe un product con código {request.Code}.");
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
                IsActive = true
            };

            long id = await productRepository.AddAsync(newProduct);
            return id;

        }

    }
}
