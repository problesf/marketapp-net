using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record DeleteProductCommand : IRequest<ProductDto>
    {
        public string Code { get; init; }
    }

    public class DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<DeleteProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.SearchByProductCode(request.Code);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product con código de producto {request.Code}");
            }

            
            await productRepository.DeleteAsync(product.Id.Value);
            return mapper.Map<ProductDto>(product);

        }

    }

}
