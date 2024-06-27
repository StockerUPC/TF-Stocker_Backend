using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Interfaces.REST.Resources;
namespace Stocker_API.Purchases.Interfaces.REST.Resources;

public record PurchaseDetailResource(int Id, PurchaseResource Purchase, ProductResource Product,decimal PurchasePrice, decimal SalePrice, int Quantity, decimal Total);

