using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;


namespace MarketNet.Application.Products.Commands
{
    public record DeleteProductCommand : IRequest<ProductDto>
    {
        public long Id { get; init; }
    }

    public class DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<DeleteProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }


            await productRepository.DeleteAsync(product.Id.Value);
            return mapper.Map<ProductDto>(product);

        }

    }

}
