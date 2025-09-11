using AutoMapper;
using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Domain.Entities.Products;
using MarketNet.src.Domain.Exceptions.Products;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Products.Queries
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
                    throw new ProductNotFoundException($"Producto con código '{request.Code}' no encontrado");
            }
            else if (request.Id.HasValue)
            {
                product = await productRepository.SearchById(request.Id.Value);
                if (product == null)
                    throw new ProductNotFoundException($"Producto con ID {request.Id.Value} no encontrado");
            }
            else
            {
                throw new ArgumentException("Debes proporcionar al menos Código o Id del producto");
            }

            return mapper.Map<ProductDto>(product);
        }


    }
}
