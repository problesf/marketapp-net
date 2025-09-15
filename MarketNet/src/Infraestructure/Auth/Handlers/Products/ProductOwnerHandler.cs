using MarketNet.Application.Common.Interfaces;           // IUserContext
using MarketNet.Infraestructure.Repositories;            // IProductRepository
using MarketNet.src.Infraestructure.Auth.Requirements.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketNet.src.Infraestructure.Auth.Handlers.Products
{
    public class ProductOwnerHandler : AuthorizationHandler<ProductOwnerRequirement>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContext _userContext;

        public ProductOwnerHandler(IProductRepository productRepository, IUserContext userContext)
        {
            _productRepository = productRepository;
            _userContext = userContext;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ProductOwnerRequirement requirement)
        {
            long userId;
            try
            {
                userId = _userContext.UserId.Value;
            }
            catch
            {
                return;
            }

            HttpContext? httpContext = context.Resource as HttpContext;
            if (httpContext is null && context.Resource is AuthorizationFilterContext afc)
                httpContext = afc.HttpContext;
            if (httpContext is null) return;

            if (!httpContext.Request.RouteValues.TryGetValue("id", out var idVal)) return;
            if (!long.TryParse(idVal?.ToString(), out var productId)) return;

            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null) return;

            if (product.SellerProfileId == userId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
