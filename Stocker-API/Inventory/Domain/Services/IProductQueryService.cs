using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Model.Queries;

namespace Stocker_API.Inventory.Domain.Services;

public interface IProductQueryService
{
    Task<Product?> Handle(GetProductByIdQuery query);
    Task<IEnumerable<Product>> Handle(GetAllProductsQuery query);
    Task<IEnumerable<Product>> Handle(GetAllProductsByCategoryIdQuery query);
}