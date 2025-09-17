using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Exceptions.Categories;
using MarketNet.Domain.Exceptions.Products;
using MarketNet.Infraestructure.Persistence.Repositories;
using MarketNet.src.Application.Products.Dto;
using MediatR;

namespace MarketNet.Application.Products.Commands
{
    public record UpdateProductCommand : IRequest<ProductDto>
    {
        public long? Id { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal? Price { get; init; }
        public int? Stock { get; init; }
        public decimal? TaxRate { get; init; }
        public string? Currency { get; init; }

        public ICollection<long>? CategoriesId { get; init; }
        public List<PAttributeDto>? Attributes { get; set; }

    }

    public class UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand, ProductDto>
    {

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            Product product = await productRepository.GetByIdAsync(request.Id.Value);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product con id {request.Id} no existe");
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
            if (request.CategoriesId is { Count: > 0 })
            {
                ICollection<Category> productCategories = new List<Category>();
                foreach (long idCategory in request.CategoriesId)
                {
                    Category category = await categoryRepository.SearchById(idCategory);
                    if (category == null)
                    {
                        throw new CategoryNotFoundException($"Categoria con ID {idCategory} no encontrado");
                    }
                    productCategories.Add(category);
                }
                product.Categories = productCategories;
            }

            await productRepository.UpdateAsync(product);
            return mapper.Map<ProductDto>(product);

        }

    }

}
