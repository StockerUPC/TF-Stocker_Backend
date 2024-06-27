namespace Stocker_API.Inventory.Interfaces.REST.Resources;

public record ProductResource(int Id, string Name, string Description, CategoryResource Category, string PhotoUrl, decimal PurchasePrice, decimal SalePrice, int Stock, DateOnly ExpiryDate);