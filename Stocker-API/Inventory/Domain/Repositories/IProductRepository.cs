using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Inventory.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> FindByCategoryIdAsync(int categoryId);
}