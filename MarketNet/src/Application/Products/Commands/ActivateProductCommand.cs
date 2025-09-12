using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record ActivateProductCommand : IRequest<ProductDto>
    {
        public string Code { get; init; }
    }

    public class ActivateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<ActivateProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.SearchByProductCode(request.Code);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product con código de producto {request.Code}");
            }
            product.IsActive = true;
            
            await productRepository.UpdateAsync(product);
            return mapper.Map<ProductDto>(product);

        }

    }

}