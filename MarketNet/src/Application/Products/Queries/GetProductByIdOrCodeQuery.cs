using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.Application.Products.Queries
{
    public record SearchProductsByProductCodeOrIdQuery : IRequest<ProductDto>
    {
        public string? Code { get; init; }
        public long? Id { get; init; }
    }

    public class SearchProductsByProductCodeOrIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<SearchProductsByProductCodeOrIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(SearchProductsByProductCodeOrIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = null;

            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                product = await productRepository.SearchByProductCode(request.Code);
                if (product == null)
                    throw new ProductNotFoundException($"Producto con c�digo '{request.Code}' no encontrado");
            }
            else if (request.Id.HasValue)
            {
                product = await productRepository.SearchById(request.Id.Value);
                if (product == null)
                    throw new ProductNotFoundException($"Producto con ID {request.Id.Value} no encontrado");
            }
            else
            {
                throw new ArgumentException("Debes proporcionar al menos C�digo o Id del producto");
            }

            return mapper.Map<ProductDto>(product);
        }


    }
}
