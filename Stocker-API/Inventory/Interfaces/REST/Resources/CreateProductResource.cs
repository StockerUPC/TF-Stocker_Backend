namespace Stocker_API.Inventory.Interfaces.REST.Resources;

public record CreateProductResource(string Name, string Description, int CategoryId, string PhotoUrl, decimal PurchasePrice, decimal SalePrice, int Stock, DateOnly ExpiryDate);