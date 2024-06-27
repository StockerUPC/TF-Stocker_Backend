using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace Stocker_API.Inventory.Interfaces.REST.Transform;

public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product product)
    {
        return new ProductResource(
            product.Id,
            product.Name,
            product.Description,
            CategoryResourceFromEntityAssembler.ToResourceFromEntity(product.Category),
            product.PhotoUrl,
            product.PurchasePrice,
            product.SalePrice,
            product.Stock,
            product.ExpiryDate);
    }
}