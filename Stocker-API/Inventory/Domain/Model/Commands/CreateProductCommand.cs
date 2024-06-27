namespace Stocker_API.Inventory.Domain.Model.Commands;

public record CreateProductCommand(string Name, string Description, int CategoryId, string PhotoUrl, decimal PurchasePrice, decimal SalePrice, int Stock, DateOnly ExpiryDate);