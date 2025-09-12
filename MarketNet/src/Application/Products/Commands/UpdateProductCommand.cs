using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record UpdateProductCommand : IRequest<ProductDto>
    {
        public string Code { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal? Price { get; init; }
        public int? Stock { get; init; }
        public decimal? TaxRate { get; init; }
        public string? Currency { get; init; }
    }

    public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.SearchByProductCode(request.Code);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product con c�digo de producto {request.Code}");
            }

            if (request.Name != null)
            {
                product.Name = request.Name;
            }

            if (request.Currency != null)
            {
                product.Currency = request.Currency;
            }

            if (request.Price != null)
            {
                product.Price = request.Price.Value;
            }

            if (request.Description != null)
            {
                product.Description = request.Description;
            }

            if (request.Stock != null)
            {
                product.Stock = request.Stock.Value;
            }

            if (request.TaxRate != null)
            {
                product.TaxRate = request.TaxRate.Value;
            }

            await productRepository.UpdateAsync(product);
            return mapper.Map<ProductDto>(product);

        }

    }

}
