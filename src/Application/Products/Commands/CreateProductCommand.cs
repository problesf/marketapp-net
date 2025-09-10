// Placeholder for CreateProductCommand

// Request
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using MarketApp.src.Domain.entities.product;
using MarketNet.src.Application.Products.Criteria;
using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Products.Queries
{
    public record CreateProductCommand : IRequest<long>
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public decimal TaxRate { get; init; }
        public string Currency { get; init; }
    }

    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand, long>
    {

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            Product newProduct = new Product
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                TaxRate = request.TaxRate,
                Stock = request.Stock,
                Currency = request.Currency,
                IsActive = true
            };

            long id = await productRepository.AddAsync(newProduct);
            return id;

        }

    }
}
