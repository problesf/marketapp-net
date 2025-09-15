using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record ActivateProductCommand : IRequest<ProductDto>
    {
        public long Id { get; init; }
    }

    public class ActivateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<ActivateProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            product.IsActive = true;

            await productRepository.UpdateAsync(product);
            return mapper.Map<ProductDto>(product);

        }

    }

}