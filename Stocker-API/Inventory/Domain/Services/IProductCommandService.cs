using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Model.Commands;

namespace Stocker_API.Inventory.Domain.Services;

public interface IProductCommandService
{
    Task<Product?> Handle(CreateProductCommand command);
    Task<Product?> Delete(Product product);
    Task<Product?> Update(Product product);
}