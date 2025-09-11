using AutoMapper;
using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Products.Queries
{
    public record SearchProductsQuery : IRequest<List<ProductDto>>
    {
        public string? Code { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public int? MinStock { get; init; }
        public int? MaxStock { get; init; }
        public decimal? MinTaxRate { get; init; }
        public decimal? MaxTaxRate { get; init; }
        public string? Currency { get; init; }
        public bool? IsActive { get; init; }

    }

    public class SearchProductsQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<SearchProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            ProductSearchCriteria criteria = new ProductSearchCriteria
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                MinStock = request.MinStock,
                MaxStock = request.MaxStock,
                MinTaxRate = request.MinTaxRate,
                MaxTaxRate = request.MaxTaxRate,
                Currency = request.Currency,
                IsActive = request.IsActive
            };

            var products = await productRepository.SearchProductsAsync(criteria);
            return mapper.Map<List<ProductDto>>(products);

        }

    }
}
