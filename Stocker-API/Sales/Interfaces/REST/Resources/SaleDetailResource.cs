using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Interfaces.REST.Resources;
namespace Stocker_API.Sales.Interfaces.REST.Resources;

public record SaleDetailResource(int Id, SaleResource Sale, ProductResource Product, decimal SalePrice, int Quantity, decimal Subtotal);

