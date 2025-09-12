using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record DeactivateProductCommand : IRequest<ProductDto>
    {
        public string Code { get; init; }
    }

    public class DeactivateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<DeactivateProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.SearchByProductCode(request.Code);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product con código de producto {request.Code}");
            }
            product.IsActive = false;
            
            await productRepository.UpdateAsync(product);
            return mapper.Map<ProductDto>(product);

        }

    }

}